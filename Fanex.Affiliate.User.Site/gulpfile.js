var gulp = require('gulp');
var jasmineBrowser = require('gulp-jasmine-browser');
var watch = require('gulp-watch');

var libraries = [
    'www/source/js/lib/jquery-3.3.1.min.js',
    'www/source/js/lib/bootstrap.min.js',
    'www/source/js/lib/jquery.validate.min.js',
    'www/source/js/lib/jquery.validate.unobtrusive.min.js',
    'www/source/js/lib/pikaday.js'
];

var commonFiles = [
    'www/source/js/site.js',
    'www/source/js/layout.js',
    'www/source/js/recheckin.js'
]

var fileToInclude = [
    ...libraries, ...commonFiles,
    'www/source/js/components/*.js',
    'www/source/js/spec/**/*.spec.js'
]

gulp.task('jasmine-phantom', function () {
    return gulp.src(fileToInclude)
      .pipe(jasmineBrowser.specRunner({ console: true }))
      .pipe(jasmineBrowser.headless({ driver: 'phantomjs' }));
});