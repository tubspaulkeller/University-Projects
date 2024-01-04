var encrypt = require('./encrypt'); 

psw = 'foo'; 
let hash = "";
let salt = "";


encrypt.hash(psw, function(result){
    hash = result.hash;
    salt = result.salt;
   // console.log("hash: ", result.hash);
    //console.log("salt: ", result.salt);
  console.log(result);
});
console.log(hash);
console.log(salt);


encrypt.compare(psw, hash, salt, function(result){
    console.log("bool result: ", result);
});