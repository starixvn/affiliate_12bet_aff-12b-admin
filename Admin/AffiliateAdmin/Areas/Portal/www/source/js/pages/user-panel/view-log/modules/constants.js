import { maxWidth } from '../../../../modules/constants';
import { globalResource } from '../../../../modules/globalResource';

export { globalResource, maxWidth }

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
    [COMMISSION_GROUP]: 'CommissionGroup',
    [USER_INFO]: 'UserInfo',
    [TRANSACTION]: 'Transaction',
    [PAYOUT]: 'Payout',
    [PLAYER]: 'Player',
    [BANNER]: 'Banner',
    [EMAIL]: 'Email',
    [USER_NOTE]: 'UserNote',
    [LOGIN_AFFILIATE]: 'LoginAffiliate',
    [LOGOUT_AFFILIATE]: 'LogoutAffiliate',
    [PASSWORD]: 'Password',
    [ADJUSTMENT]: 'Adjustment',
    [MEMBER]: 'Member' 
}

export const headerText = {
    Type: 'Type',
    Index: '#',
    Id: 'ID',
    Date: 'Date',
    UpdatedBy: 'Updated By',
    Action: 'Action',
    Description: 'Description',
    Ip: 'IP'
}