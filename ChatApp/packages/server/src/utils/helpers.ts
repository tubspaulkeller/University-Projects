export class Helpers {
  public static firstUpper(str: string) {
    const name = str.toLocaleLowerCase();
    return name.charAt(0).toUpperCase() + name.slice(1);
  }

  public static lowerCase(str: string) {
    return str.toLocaleLowerCase();
  }
}
