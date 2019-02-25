const COMMISSION_GROUP = 1;
const USER_INFO = 2;
const TRANSACTION = 3;
const PAYOUT = 4;
const PERMISSION = 5;
const PLAYER = 6;
const BANNER = 7;
const EMAIL = 8;
const MESSAGE = 9;
const EXCHANGE_RATE = 10;
const USER_NOTE = 11;
const LOGIN_ADMIN = 12;
const LOGIN_AFFILIATE = 13;
const LOGOUT_ADMIN = 14;
const LOGOUT_AFFILIATE = 15;
const PASSWORD = 16;
const ADJUSTMENT = 17;
const MEMBER = 18;
const USER_CONTROL = 19;

export const logTypes = {
    CommissionGroup: COMMISSION_GROUP,
    UserInfo: USER_INFO,
    Transaction: TRANSACTION,
    Payout: PAYOUT,
    Player: PLAYER,
    Banner: BANNER,
    Email: EMAIL,
    UserNote: USER_NOTE,
    LoginAffiliate: LOGIN_AFFILIATE,
    LogoutAffiliate: LOGOUT_AFFILIATE,
    Password: PASSWORD,
    Adjustment: ADJUSTMENT,
    Member: MEMBER
}

export const headerText = {
    Type: 'Type',
    Index: '#',
    Id: 'ID',
    Date: 'Date',
    UpdatedBy: 'Updated By',
    Action: 'Action',
    Description: 'Description'
}