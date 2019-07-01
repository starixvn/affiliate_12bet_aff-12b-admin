import * as actions from '../index';
import * as types from '../types';
import * as ajaxRequest from '../../../../../modules/ajaxRequest';

const runTestAction = ({ name, type, data }) => {
    it(`should create action to ${name}`, () => {
        const expectedAction = { type, data };

        expect(actions[name](data)).toEqual(expectedAction);
    });
}

const actionList = [
    { name: 'setAffName', type: types.SET_AFF_NAME, data: '12BETAFF226' },
    { name: 'setIsValidUserName', type: types.SET_IS_VALID_USERNAME, data: true },
    { name: 'toggleLoaderLeftPanel', type: types.TOGGLE_LOADER_LEFT_PANEL, data: true },
]

describe('left panel actions', () => {
    actionList.forEach(item => {
        runTestAction(item);
    });

    it('should create action to getOnLineList with correct parameter', async () => {
        const expectedResponse = {
            data: [],
            status: 200
        };

        ajaxRequest.post = jest.fn(() => Promise.resolve(expectedResponse));

        const param = {
            affUsername: "12BETAFF226"
        }
        
        const result = await actions['getOnLineList'](param.affUsername)();

        expect(ajaxRequest.post).toHaveBeenCalledWith('Portal/CustomerLeftPanel/getOnLineList', param);
        expect(result).toEqual(expectedResponse.data);
    })

    it('should create action to getOnLineList with incorrect parameter', async () => {
        ajaxRequest.post = jest.fn();
        const param = {
            affUsername: "12BET"
        }

        const result = await actions['getOnLineList'](param.affUsername)();

        expect(ajaxRequest.post).toHaveBeenCalledWith('Portal/CustomerLeftPanel/getOnLineList', param);

        expect(result).toEqual(null);
    })

    it('should create action to updateLanguage with correct parameter', async () => {
        const expectedResponse = {
            data: {
                Code: 0,
                IsSuccessful: true,
                Message: ""
            },
            status: 200
        };

        ajaxRequest.post = jest.fn(() => Promise.resolve(expectedResponse));

        const param = {
            AffId: 1271808,
            PreferredLanguage: "vi-VN"
        }
        
        const result = await actions['updateLanguage'](param)();

        expect(ajaxRequest.post).toHaveBeenCalledWith('Portal/CustomerLeftPanel/UpdateLanguage', param);
        expect(result).toEqual(expectedResponse.data);
    })

    it('should create action to updateLanguage with incorrect parameter', async () => {
        ajaxRequest.post = jest.fn();
        const param = {
            AffId: 1271808,
            PreferredLanguage: "abc"
        }

        const result = await actions['updateLanguage'](param)();

        expect(ajaxRequest.post).toHaveBeenCalledWith('Portal/CustomerLeftPanel/UpdateLanguage', param);

        expect(result).toEqual(null);
    })

    it('should create action to updateAffType with correct parameter', async () => {
        const expectedResponse = {
            data: {
                Code: 0,
                IsSuccessful: true,
                Message: ""
            },
            status: 200
        };

        ajaxRequest.post = jest.fn(() => Promise.resolve(expectedResponse));

        const param = {
            AffId: 1271808,
            AffType: "2"
        }
        
        const result = await actions['updateAffType'](param)();

        expect(ajaxRequest.post).toHaveBeenCalledWith('Portal/CustomerLeftPanel/UpdateAffType', param);
        expect(result).toEqual(expectedResponse.data);
    })

    it('should create action to updateAffType with incorrect parameter', async () => {
        ajaxRequest.post = jest.fn();
        const param = {
            AffId: 1271808,
            AffType: "-2"
        }

        const result = await actions['updateAffType'](param)();

        expect(ajaxRequest.post).toHaveBeenCalledWith('Portal/CustomerLeftPanel/UpdateAffType', param);

        expect(result).toEqual(null);
    })

    it('should create action to updateStatus with correct parameter', async () => {
        const expectedResponse = {
            data: {
                Code: 0,
                IsSuccessful: true,
                Message: ""
            },
            status: 200
        };

        ajaxRequest.post = jest.fn(() => Promise.resolve(expectedResponse));

        const param = {
            AffId: 92037914,
            Status: "Disapproved"
        }
        
        const result = await actions['updateStatus'](param)();

        expect(ajaxRequest.post).toHaveBeenCalledWith('Portal/CustomerLeftPanel/UpdateStatus', param);
        expect(result).toEqual(expectedResponse.data);
    })

    it('should create action to updateStatus with incorrect parameter', async () => {
        ajaxRequest.post = jest.fn();
        const param = {
            AffId: 123456,
            Status: "Disapproved"
        }

        const result = await actions['updateStatus'](param)();

        expect(ajaxRequest.post).toHaveBeenCalledWith('Portal/CustomerLeftPanel/UpdateStatus', param);

        expect(result).toEqual(null);
    })

    it('should create action to updateClosedStatus with correct parameter', async () => {
        const expectedResponse = {
            data: {
                Code: 0,
                IsSuccessful: true,
                Message: ""
            },
            status: 200
        };

        ajaxRequest.post = jest.fn(() => Promise.resolve(expectedResponse));

        const param = {
            AffId: 1271808,
            IsClosed: true
        }
        
        const result = await actions['updateClosedStatus'](param)();

        expect(ajaxRequest.post).toHaveBeenCalledWith('Portal/CustomerLeftPanel/UpdateClosedStatus', param);
        expect(result).toEqual(expectedResponse.data);
    })

    it('should create action to updateClosedStatus with incorrect parameter', async () => {
        ajaxRequest.post = jest.fn();
        const param = {
            AffId: 123456,
            IsClosed: true
        }

        const result = await actions['updateClosedStatus'](param)();

        expect(ajaxRequest.post).toHaveBeenCalledWith('Portal/CustomerLeftPanel/UpdateClosedStatus', param);

        expect(result).toEqual(null);
    })
});