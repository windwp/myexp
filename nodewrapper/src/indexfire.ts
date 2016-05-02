import Firebase=require('firebase');

var fRef = new Firebase('https://danhngonvietnam.firebaseio.com/content');
fRef.orderByKey().equalTo("1000").on("value", function (snapshot) {
  console.log(snapshot.val());
}, function (errorObject) {
  console.log("The read failed: " + errorObject.code);
});



