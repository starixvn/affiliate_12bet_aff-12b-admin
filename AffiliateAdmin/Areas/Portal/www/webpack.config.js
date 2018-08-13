const path = require("path");
const webpack = require("webpack");
const MiniCssExtractPlugin = require("mini-css-extract-plugin");
const OptimizeCSSAssetsPlugin = require("optimize-css-assets-webpack-plugin");
const merge = require("webpack-merge");
const CleanWebpackPlugin = require("clean-webpack-plugin");

const { environment, projectPath } = require("./config-utils/constant");
const entry = require("./config-utils/entry");

let pathsToClean = ["dist/js/**/*.*", "dist/style/**/*.*"];

let cleanOptions = {
    root: path.resolve(__dirname, "."),
    verbose: true,
    dry: false
};

module.exports = env => {
    env = environment[env] || environment.development;
    const isDevBuild = !(env === environment.production);

    const sharedConfig = () => ({
        mode: env,
        stats: { modules: false },
        resolve: { extensions: [".js", ".jsx"] },
        output: {
            filename: "[name].js",
            publicPath: '/dist/'
        },
        module: {
            rules: [{
                    test: /\.(js|jsx)$/,
                    use: "babel-loader",
                    exclude: /(node_modules|bower_components)/
                },
                {
                    test: /\.(png|jpg|jpeg|gif|svg|woff|woff2|eot|ttf)$/,
                    use: [{
                        loader: "url-loader",
                        options: {
                            limit: 4096,
                            name: "[name].[ext]",
                            outputPath: "style/assets/",
                            publicPath: "assets/"
                        }
                    }]
                }
            ]
        }
    });

    // Configuration for client-side bundle suitable for running in browsers
    const clientBundleOutputDir = projectPath.clientOutputPath;
    const clientBundleConfig = merge(sharedConfig(), {
        entry: entry,
        module: {
            rules: [{
                test: /\.s?[ac]ss$/,
                use: [
                    "style-loader",
                    MiniCssExtractPlugin.loader,
                    "css-loader",
                    "sass-loader"
                ]
            }]
        },
        output: { path: clientBundleOutputDir },
        plugins: [
            new MiniCssExtractPlugin({
                filename: "[name].css"
            }),
            new webpack.DllReferencePlugin({
                context: __dirname,
                manifest: require(`${clientBundleOutputDir}/vendors/react-manifest.json`)
            }),
            new webpack.DllReferencePlugin({
                context: __dirname,
                manifest: require(`${clientBundleOutputDir}/vendors/reactstrap-manifest.json`)
            })
        ].concat(isDevBuild ? [] : [new OptimizeCSSAssetsPlugin(), new CleanWebpackPlugin(pathsToClean, cleanOptions), ]),
        devtool: isDevBuild ? "source-map" : undefined
    });

    return [clientBundleConfig];
};