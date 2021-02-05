jest.mock('redux');
jest.mock('react-redux');
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import * as actions from '../../../actions/index';

describe('mass adjustment - UploadResult - connect', () => {
    afterAll(() => {
        jest.resetAllMocks();
    })

    let mapStateToProps, mapDispatchToProps;
    const connectMock = jest.fn();

    beforeAll(() => {
        connect.mockImplementationOnce((mapStateToPropsFunc, mapDispatchToPropsFunc) => {
            mapStateToProps = mapStateToPropsFunc;
            mapDispatchToProps = mapDispatchToPropsFunc;
            return connectMock;
        });
        require('../../UploadResult').default;
    });

    it('props should be mapped correctly', () => {
        const expectedResult = { isClickUpload: true, file: { name: 'abc.xlsx' } };
        const state = {
            isClickUpload: true,
            file: { name: 'abc.xlsx' },
            anotherProp: "Not included"
        };

        const result = mapStateToProps(state);

        expect(result).toEqual(expectedResult);
    })

    it('bindActionCreators should be called with correct parameter', () => {
        const mockBindAction = jest.fn();
        bindActionCreators.mockImplementationOnce(mockBindAction);
        const expectedActionCreator = {
            setFile: actions['setFile'],
            setIsClickUpload: actions['setIsClickUpload'],
            setIsShowResult: actions['setIsShowResult'],
            toggleLoader: actions['toggleLoader'],
            handleUploadFile: actions['handleUploadFile']
        }

        const dispatch = {};
        mapDispatchToProps(dispatch);

        expect(mockBindAction).toHaveBeenCalledWith(expectedActionCreator, dispatch);
    })

    it('returned function of connect should be called with correct parameter', () => {
        const UploadResult = require('../../UploadResult').UploadResult;

        expect(connectMock).toHaveBeenCalledWith(UploadResult);
    })
})