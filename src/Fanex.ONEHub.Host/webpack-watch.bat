@echo off
echo Starting webpack in watch mode...
echo Current directory: %CD%
node node_modules/webpack/bin/webpack.js --config Areas/Portal/www/webpack.config.js --watch
