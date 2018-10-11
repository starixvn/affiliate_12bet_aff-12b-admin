const path = require("path");

const rootPath = path.resolve(__dirname, '../source')

module.exports = {
    environment: {
        development: 'development',
        production: 'production'
    },
    projectPath: {
        clientInputPath: path.resolve(__dirname, '../source'),
        clientOutputPath: path.resolve(__dirname, '../dist')
    }
}