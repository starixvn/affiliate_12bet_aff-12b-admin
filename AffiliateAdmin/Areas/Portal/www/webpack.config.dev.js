const path = require("path");
const webpack = require("webpack");
const CleanWebpackPlugin = require('clean-webpack-plugin');

const { environment } = require("./config-utils/constant");
const config = require("./webpack.config");

const devConfig = config(environment.development)[0];
devConfig.devServer = {
    port: 9000,
    hot: true,
    watchContentBase: true,
    proxy: {
        '/Portal': {
            target: 'http://localhost:65181/Portal',
            pathRewrite: { '^/Portal': '' }
        },
        '/GroupSetting': {
            target: 'http://localhost:65181/GroupSetting',
            pathRewrite: { '^/GroupSetting': '' }
        },
        '/App_Js': {
            target: 'http://localhost:65181/App_Js',
            pathRewrite: { '^/App_Js': '' }
        },
        '/Handler': {
            target: 'http://localhost:65181/Handler',
            pathRewrite: { '^/Handler': '' }
        },
        '/App_Themes': {
            target: 'http://localhost:65181/App_Themes',
            pathRewrite: { '^/App_Themes': '' }
        }
    }
};


module.exports = devConfig;