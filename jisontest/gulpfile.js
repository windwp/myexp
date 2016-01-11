var gulp = require('gulp');
var shell=require('gulp-shell');
var del = require('del')
gulp.task('watch', function() {
    return gulp.watch(['*.json','*.txt',"spec/*.js"], ['clean','compile']);
});
gulp.task('clean',function(){
    return del(['calc.js']);
});
gulp.task('compile',shell.task([
    'cls',
  'jison calc.txt',
//   'node index.js',
  'jasmine-node spec'
]))