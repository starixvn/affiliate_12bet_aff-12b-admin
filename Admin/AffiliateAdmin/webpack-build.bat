@echo off
echo Building webpack in production mode...
echo Current directory: %CD%
node node_modules/webpack/bin/webpack.js --config Areas/Portal/www/webpack.config.js --env=production
