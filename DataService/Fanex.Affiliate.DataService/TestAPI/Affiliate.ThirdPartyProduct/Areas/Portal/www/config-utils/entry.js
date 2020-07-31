const { projectPath } = require("./constant");

const jsDist = projectPath.relativeOutputPath + "/js";
const styleDist = projectPath.relativeOutputPath + "/style";

const clientRoot = projectPath.clientInputPath;

module.exports = {
    [`${jsDist}/pages/commission/commission-group`]: clientRoot + "/js/pages/commission-group/index.jsx",
    [`${jsDist}/pages/commission/assign-group`]: clientRoot + "/js/pages/commission-group/components/assign-group/index.jsx",
    [`${jsDist}/pages/commission/assign-private-group`]: clientRoot + "/js/pages/commission-group/components/assign-private-group/index.jsx",
    [`${jsDist}/pages/report/affiliate-report`]: clientRoot + "/js/pages/affiliate-report/index.jsx",
    [`${jsDist}/pages/media-gallery/landing-page`]: clientRoot + "/js/pages/media-gallery/landing-page/index.jsx",
    [`${jsDist}/pages/affiliate/affiliate-info`]: clientRoot + "/js/pages/affiliate-info/index.jsx",
    [`${jsDist}/pages/payment/payment-request`]: clientRoot + "/js/pages/payment-request/index.jsx",
    [`${jsDist}/pages/customer/customer`]: clientRoot + "/js/pages/customer/index.jsx",
    [`${jsDist}/pages/affiliate/email-history`]: clientRoot + "/js/pages/email-history/index.jsx",

    //For Thirdparty Product pages
    [`${jsDist}/pages/thirdparty-product/betsoft`]: clientRoot + "/js/pages/thirdparty-product/betsoft/index.jsx",
    [`${jsDist}/pages/thirdparty-product/ebet`]: clientRoot + "/js/pages/thirdparty-product/ebet/index.jsx",
    [`${jsDist}/pages/thirdparty-product/crystal-gpi`]: clientRoot + "/js/pages/thirdparty-product/crystal-gpi/index.jsx",
    [`${jsDist}/pages/thirdparty-product/isoftbet`]: clientRoot + "/js/pages/thirdparty-product/isoftbet/index.jsx",
    [`${jsDist}/pages/thirdparty-product/opus`]: clientRoot + "/js/pages/thirdparty-product/opus/index.jsx",
    [`${jsDist}/pages/thirdparty-product/importqueue`]: clientRoot + "/js/pages/thirdparty-product/importqueue/index.jsx",
    [`${jsDist}/pages/thirdparty-product/timetracker`]: clientRoot + "/js/pages/thirdparty-product/timetracker/index.jsx",
    [`${jsDist}/pages/thirdparty-product/exchange-summary-transaction`]: clientRoot + "/js/pages/thirdparty-product/exchange-summary-transaction/index.jsx",

    [`${styleDist}/portalmain`]: clientRoot + "/style/main.scss",
    [`${styleDist}/layout`]: clientRoot + "/style/layout/main-layout.scss",
    [`${styleDist}/user-panel-layout`]: clientRoot + "/style/layout/user-panel-layout.scss",
    [`${styleDist}/pages/commission-group`]: clientRoot + "/style/pages/commission-group.scss",
    [`${styleDist}/pages/affiliate-report`]: clientRoot + "/style/pages/affiliate-report.scss",
    [`${styleDist}/pages/media-gallery/landing-page`]: clientRoot + "/style/pages/media-gallery/landing-page.scss",
    [`${styleDist}/pages/affiliate-info`]: clientRoot + "/style/pages/affiliate-info.scss",
    [`${styleDist}/pages/payment/payment-request`]: clientRoot + "/style/pages/payment/payment-request.scss",
    [`${styleDist}/pages/customer`]: clientRoot + "/style/pages/customer/main.scss",
    [`${styleDist}/pages/email-history`]: clientRoot + "/style/pages/email-history.scss",
};