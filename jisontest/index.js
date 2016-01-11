
var fs=require("fs");
var Parser = require("jison").Parser;
console.log('===========================================')
fs.readFile('calculator.json', function (err, data) {
   if (err) {
       return console.error(err);
   }
var grammar=JSON.parse(data.toString());
var parser = new Parser(grammar);

// generate source, ready to be written to disk
var parserSource = parser.generate();
fs.writeFile('calculator.js', parserSource,()=>{
   console.log('done'); 
});
});

// throws lexical error
       
