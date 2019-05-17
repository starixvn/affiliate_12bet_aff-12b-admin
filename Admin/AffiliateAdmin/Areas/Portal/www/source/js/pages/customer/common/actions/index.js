import { RESET_USER_PANEL } from './types';

export const resetUserPanel = (data) => {
    return {
        type: RESET_USER_PANEL,
        data
    }
}