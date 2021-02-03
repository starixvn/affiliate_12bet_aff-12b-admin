import * as actions from '../index';
import * as types from '../types';
import * as ajaxRequest from '../../../../../modules/ajaxRequest';
import { globalResource } from '../../../../../modules/globalResource';

const runTestAction = ({ name, type, data }) => {
    it(`should create action to ${name}`, () => {
        const expectedAction = { type, data };

        expect(actions[name](data)).toEqual(expectedAction);
    });
}

const actionList = [
    { name: 'setFile', type: types.SET_FILE, data: null },
    { name: 'setIsClickUpload', type: types.SET_IS_CLICK_UPLOAD, data: true },
    { name: 'setIsShowResult', type: types.SET_IS_SHOW_RESULT, data: true },
]

describe('mass adjustment actions', () => {
    afterAll(() => {
        jest.resetAllMocks();
    })

    actionList.forEach(item => {
        runTestAction(item);
    });

    it('should create action to handleUploadFile with correct parameter', async () => {
        const expectedResponse = {
            data: {
                Result: {
                    IsSuccessful: true,
                    Message: '',
                },
                ErroredAdjustmentInfoList: null
            }, status: 200
        };

        ajaxRequest.post = jest.fn(() => Promise.resolve(expectedResponse));

        const file = {
            name: "abc.xlsx"
        }

        const result = await actions['handleUploadFile'](file)();

        expect(ajaxRequest.post.mock.calls[0][0]).toEqual('Portal/Adjustment/UploadFile');
        expect(result).toEqual(expectedResponse.data);
    })

    it('should create action to handleUploadFile with error response', async () => {
        ajaxRequest.post = jest.fn();

        const file = {
            name: "abc.xlsx"
        }

        const result = await actions['handleUploadFile'](file)();

        expect(ajaxRequest.post.mock.calls[0][0]).toEqual('Portal/Adjustment/UploadFile');
        expect(result).toEqual({
            Result: {
                IsSuccessful: false,
                Message: globalResource.AnError,
            },
            ErroredAdjustmentInfoList: null
        });
    })
});