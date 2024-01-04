import { Component, Inject, Input, OnDestroy, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../../../environments/environment';
import { filter } from 'rxjs';
import { ChatInputProps } from '../chat-input.component';
import { SocketEvent } from '../../../../Lib/SocketEvent';
import { DataService } from '../../../../Services/data.service';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'app-gif-search',
  templateUrl: './gif-search.component.html',
  styleUrls: ['./gif-search.component.scss'],
})
export class GifSearchComponent implements OnInit, OnDestroy {
  query = '';
  gifs: any[] = [];
  trendingSubRef: any;
  searchSubRef: any;

  @Input() close!: () => void;

  constructor(
    @Inject(MAT_DIALOG_DATA) public inputProps: ChatInputProps,
    public dialogRef: MatDialogRef<GifSearchComponent>,
    private http: HttpClient,
    private dataService: DataService
  ) {}

  private trending = `https://api.giphy.com/v1/gifs/trending?api_key=${environment.gif_key}&limit=25&rating=R`;

  ngOnInit() {
    this.trendingSubRef = this.http.get(this.trending).subscribe((response: any) => {
      this.gifs = response.data;
    });
  }

  ngOnDestroy() {
    this.trendingSubRef?.unsubscribe();
    this.searchSubRef?.unsubscribe();
  }

  search() {
    const endpoint = `https://api.giphy.com/v1/gifs/search?api_key=${environment.gif_key}&q=${this.query}&limit=25&rating=R&lang=en`;
    this.searchSubRef = this.http
      .get(endpoint)
      .pipe(filter((response: any) => response.data.length > 0 && this.query !== ''))
      .subscribe((response: any) => {
        this.gifs = response.data;
      });

    this.trendingSubRef = this.http
      .get(this.trending)
      .pipe(filter(() => this.query === ''))
      .subscribe((response: any) => {
        this.gifs = response.data;
      });
  }

  sendGif(gif: any): void {
    console.log(this.inputProps);
    this.dataService.sendMessage({
      type: SocketEvent.Chat.SEND_MESSAGE_GIF,
      groupName: this.inputProps?.currentGroup.name,
      groupId: this.inputProps?.currentGroup.id,
      sender: this.inputProps?.currentUser._id,
      receiver: this.inputProps?.receiverId,
      message: '',
      gif: gif.images.original.url,
      file: null,
      ianaTimezone: Intl.DateTimeFormat().resolvedOptions().timeZone,
      timestamp: new Date().toISOString(),
    });
    this.dialogRef.close();
  }
}
