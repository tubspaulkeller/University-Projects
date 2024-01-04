var crypto = require('crypto'); 

// Salt
var getRandomSalt = function (callback) {
	return callback(crypto.randomBytes(10).toString('hex'));
};

// Pepper
var getRandomPepper = function (callback) {
	// generate random string
	var alphabets = "abcdefghijklmnopqrstuvwxyzABCDEFGHIKLMNOPQRSTUVWXYZ0123456789"
	return callback(alphabets[Math.floor(Math.random() * alphabets.length)]);
};

module.exports = {
	hash: function (plainOpenPassword, callback) {
		getRandomSalt(function (salt) {
			getRandomPepper(function (pepper) {
				var newPasswordToHash = pepper + salt + plainOpenPassword + salt + pepper;
        console.log("Pepper: ", pepper);
        console.log("newPasswordToHash", newPasswordToHash);
				return callback({
					"hash": crypto.createHash('sha256').update(newPasswordToHash).digest('hex'),
					"salt": salt
				});
			});
		});
	},

	compare: function (plainOpenPassword, hashFromPasswordStore, salt, callback) {
		// generate random string
		var alphabets = "abcdefghijklmnopqrstuvwxyzABCDEFGHIKLMNOPQRSTUVWXYZ0123456789";
		
		for (var i = 0; i < alphabets.length; i++) {
			var newPasswordToHash = alphabets[i] + salt + plainOpenPassword + salt + alphabets[i];
			var hash = crypto.createHash('sha256').update(newPasswordToHash).digest('hex');
			if (hash === hashFromPasswordStore) {
				return callback(true);
			}
		}
		return callback(false);
	}
};
