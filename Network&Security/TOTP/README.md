# Login with TOTP 

## description:
### Lab8
1. Install  XAMPP 
2. Open XAMPP 
3. Configure Port of MySql to 3310 in Portsection and configfile 
4. Open http://localhost/phpmyadmin/
5. Move to the config directory in LAB8-TOTP repo
6. Open database.js: enter your credentials 
7. Open Terminal
8. Move to the scripts folder in the repo 
9. Type into the terminal: node create_database.js 
10. cd ../ 
11. npm i 
12. node index.js

### participants:
Paul Keller 

### course:
Network & Security

### semester:
SoSe2022


## Evaluation

### How secure do you think your own implementation is?
- Attack Vectors:
  - Hash collision attack - brute force/calculate hashs of known passwords
    - Not irreversible, just hard to reverse
    - Dictionary Attacks
      - Usually prevented by pepper 
  - Pre-computed tables (rainbow tables), can be used as a lookup
    - Is prevented by use of salts
  - 2fa:
    - Supply chain attacks, could intercept secret during setup if device is infected
    - Man in the middle attack, could read secret as well as additional information
    - Secret is stored in plain text in the db, if db is breached the information is leaked
    - Secret is written into session, sessions can be hijacked

### Why not to use pepper
- [Stackoverflow discussion](https://stackoverflow.com/questions/16891729/best-practices-salting-peppering-passwords)

### Which security goals did you achieve with your implementation and which not?
- Achieved:
  - No plain text passwords in db
  - Use of hashing for additional security
    - makes simple passwords more complex / unique for every user
  - Simple TOTP / 2FA in addition to normal password
- Not achieved:
  - Route protection
    - Currently not redirecting on successful login, simply rendering different .html file
    - Ideally should redirect to a protected route, normally only accessible with valid token / session
  - Proper auth flow with token