const { projectPath } = require("./constant");

const jsDist = projectPath.relativeOutputPath + "/js";
const styleDist = projectPath.relativeOutputPath + "/style";

const clientRoot = projectPath.clientInputPath;

module.exports = {
    [`${jsDist}/pages/commission/commission-group`]: clientRoot + "/js/pages/commission-group/index.jsx",
    [`${jsDist}/pages/commission/assign-group`]: clientRoot + "/js/pages/commission-group/components/assign-group/index.jsx",
    [`${jsDist}/pages/commission/assign-private-group`]: clientRoot + "/js/pages/commission-group/components/assign-private-group/index.jsx",
    [`${jsDist}/pages/Recheckin/recheckin`]: clientRoot + "/js/pages/RecheckIn/recheckin",
    [`${jsDist}/pages/layout/pagelayout`]: clientRoot + "/js/pagelayout/pagelayout.js",
    [`${jsDist}/pages/report/affiliate-report`]: clientRoot + "/js/pages/affiliate-report/index.jsx",
    [`${jsDist}/pages/userpanel/daily-earning`]: clientRoot + "/js/pages/user-panel/daily-earning/index.jsx",
    [`${jsDist}/pages/userpanel/daily-earning-detail`]: clientRoot + "/js/pages/user-panel/daily-earning-detail/index.jsx",
    [`${jsDist}/pages/userpanel/player-detail`]: clientRoot + "/js/pages/user-panel/player-detail/index.jsx",
    [`${jsDist}/pages/media-gallery/landing-page`]: clientRoot + "/js/pages/media-gallery/landing-page/index.jsx",
    [`${jsDist}/pages/customer/customer-statistics`]: clientRoot + "/js/pages/customer/customer-statistics/index.jsx",
    [`${jsDist}/pages/userpanel/customer-member`]: clientRoot + "/js/pages/user-panel/customer-member/index.jsx",
    [`${jsDist}/pages/affiliate/affiliate-info`]: clientRoot + "/js/pages/affiliate-info/index.jsx",
    [`${jsDist}/pages/userpanel/note`]: clientRoot + "/js/pages/user-panel/note/index.jsx",
    [`${jsDist}/pages/userpanel/tracking-statistics`]: clientRoot + "/js/pages/user-panel/tracking-statistics/index.jsx",
    [`${jsDist}/pages/userpanel/referrers`]: clientRoot + "/js/pages/user-panel/referrers/index.jsx",
    [`${jsDist}/pages/userpanel/landing-page-statistics`]: clientRoot + "/js/pages/user-panel/landing-page-statistics/index.jsx",
    [`${jsDist}/pages/userpanel/view-log`]: clientRoot + "/js/pages/user-panel/view-log/index.jsx",
    [`${jsDist}/pages/userpanel/adjustment`]: clientRoot + "/js/pages/user-panel/adjustment/index.jsx",
    [`${jsDist}/pages/userpanel/statement-history`]: clientRoot + "/js/pages/user-panel/statement-history/index.jsx",
    [`${jsDist}/pages/payment/payment-request`]: clientRoot + "/js/pages/payment-request/index.jsx",

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
    [`${styleDist}/pages/user-panel/player-detail`]: clientRoot + "/style/pages/user-panel/player-detail.scss",
    [`${styleDist}/pages/media-gallery/landing-page`]: clientRoot + "/style/pages/media-gallery/landing-page.scss",
    [`${styleDist}/pages/customer-statistics`]: clientRoot + "/style/pages/customer-statistics.scss",
    [`${styleDist}/pages/user-panel/customer-member`]: clientRoot + "/style/pages/user-panel/customer-member.scss",
    [`${styleDist}/pages/affiliate-info`]: clientRoot + "/style/pages/affiliate-info.scss",
    [`${styleDist}/pages/user-panel/note`]: clientRoot + "/style/pages/user-panel/note.scss",
    [`${styleDist}/pages/user-panel/referrers`]: clientRoot + "/style/pages/user-panel/referrers.scss",
    [`${styleDist}/pages/user-panel/landing-page-statistics`]: clientRoot + "/style/pages/user-panel/landing-page-statistics.scss",
    [`${styleDist}/pages/user-panel/view-log`]: clientRoot + "/style/pages/user-panel/view-log.scss",
    [`${styleDist}/pages/user-panel/adjustment`]: clientRoot + "/style/pages/user-panel/adjustment.scss",
    [`${styleDist}/pages/user-panel/statement-history`]: clientRoot + "/style/pages/user-panel/statement-history.scss",
    [`${styleDist}/pages/payment-request`]: clientRoot + "/style/pages/payment-request.scss",
};