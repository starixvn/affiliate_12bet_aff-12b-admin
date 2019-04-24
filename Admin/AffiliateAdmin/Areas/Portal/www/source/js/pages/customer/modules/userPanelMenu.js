import React from 'react';
import statementHistoryReducer from '../../user-panel/statement-history/reducers';
import userAccountReducer from '../../user-panel/user-account/reducers';
import noteReducer from '../../user-panel/note/reducers';
import adjustmentReducer from '../../user-panel/adjustment/reducers';
import trackingStatisticsReducer from '../../user-panel/tracking-statistics/reducers';
import memberReducer from '../../user-panel/customer-member/reducers';
import viewLogReducer from '../../user-panel/view-log/reducers';
import referrerStatisticsReducer from '../../user-panel/referrers/reducers';
import landingPageStatisticsReducer from '../../user-panel/landing-page-statistics/reducers';

const StatementHistory = React.lazy(() =>
    import ( /* webpackChunkName: "statementHistory" */ '../../user-panel/statement-history/components/AppContainer'));
const DailyEarning = React.lazy(() =>
    import ( /* webpackChunkName: "dailyEarning" */ '../../user-panel/daily-earning/components/AppContainer'));
const DailyEarningDetail = React.lazy(() =>
    import ( /* webpackChunkName: "dailyEarningDetail" */ '../../user-panel/daily-earning-detail/components/AppContainer'));
const UserAccount = React.lazy(() =>
    import ( /* webpackChunkName: "userAccount" */ '../../user-panel/user-account/components/AppContainer'));
const Note = React.lazy(() =>
    import ( /* webpackChunkName: "note" */ '../../user-panel/note/components/AppContainer'));
const PlayerDetail = React.lazy(() =>
    import ( /* webpackChunkName: "playerDetail" */ '../../user-panel/player-detail/components/AppContainer'));
const Adjustment = React.lazy(() =>
    import ( /* webpackChunkName: "adjustment" */ '../../user-panel/adjustment/components/AppContainer'));
const TrackingStatistics = React.lazy(() =>
    import ( /* webpackChunkName: "trackingStatistics" */ '../../user-panel/tracking-statistics/components/AppContainer'));
const Member = React.lazy(() =>
    import ( /* webpackChunkName: "member" */ '../../user-panel/customer-member/components/AppContainer'));
const ViewLog = React.lazy(() =>
    import ( /* webpackChunkName: "viewLog" */ '../../user-panel/view-log/components/AppContainer'));
const ReferrerStatistics = React.lazy(() =>
    import ( /* webpackChunkName: "referrerStatistics" */ '../../user-panel/referrers/components/AppContainer'));
const LandingPageStatistics = React.lazy(() =>
    import ( /* webpackChunkName: "landingPageStatistics" */ '../../user-panel/landing-page-statistics/components/AppContainer'));

export const menuKey = {
    statementHistory: 'StatementHistory',
    dailyEarning: 'dailyEarning',
    dailyEarningDetail: 'dailyEarningDetail',
    edit: 'edit',
    note: 'note',
    playerDetail: 'playerDetail',
    adjustment: 'adjustment',
    trackingStatistics: 'trackingStatistics',
    member: 'member',
    viewLog: 'viewLog',
    clickStatistics: 'clickStatistics',
    landingPageStatistics: 'landingPageStatistics'
}

export const userPanelMenuList = [
    { key: menuKey.statementHistory, name: 'Statement History', component: StatementHistory, isChild: false },
    { key: menuKey.dailyEarning, name: '', component: DailyEarning, isChild: true },
    { key: menuKey.dailyEarningDetail, name: '', component: DailyEarningDetail, isChild: true },
    { key: menuKey.edit, name: 'Edit', component: UserAccount, isChild: false },
    { key: menuKey.note, name: 'Note', component: Note, isChild: false },
    { key: menuKey.playerDetail, name: 'Players Detail', component: PlayerDetail, isChild: false },
    { key: menuKey.adjustment, name: 'Adjustment', component: Adjustment, isChild: false },
    { key: menuKey.trackingStatistics, name: 'Tracking Statistics', component: TrackingStatistics, isChild: false },
    { key: menuKey.member, name: 'Member', component: Member, isChild: false },
    { key: menuKey.viewLog, name: 'View Log', component: ViewLog, isChild: false },
    { key: menuKey.clickStatistics, name: 'Click Statistics', component: ReferrerStatistics, isChild: false },
    { key: menuKey.landingPageStatistics, name: '', component: LandingPageStatistics, isChild: true }
]

export const userPanelReducer = {
    [menuKey.statementHistory]: statementHistoryReducer,
    [menuKey.dailyEarning]: userAccountReducer,
    [menuKey.dailyEarningDetail]: null,
    [menuKey.edit]: userAccountReducer,
    [menuKey.note]: noteReducer,
    [menuKey.playerDetail]: null,
    [menuKey.adjustment]: adjustmentReducer,
    [menuKey.trackingStatistics]: trackingStatisticsReducer,
    [menuKey.member]: memberReducer,
    [menuKey.viewLog]: viewLogReducer,
    [menuKey.clickStatistics]: referrerStatisticsReducer,
    [menuKey.landingPageStatistics]: landingPageStatisticsReducer
}