import React from 'react';
import { UploadResult } from '../UploadResult';
import { shallow } from 'enzyme';
import { incorrectFormatMessage } from '../../libs/constants';

describe('mass adjustment - components - UploadResult ', () => {
    afterAll(() => {
        jest.resetAllMocks();
    })

    const defaultResponse = {
        Result: {
            IsSuccessful: true,
            Message: '',
        },
        ErroredAdjustmentInfoList: null
    }

    const defaultProps = {
        setFile: jest.fn(),
        setIsClickUpload: jest.fn(),
        setIsShowResult: jest.fn(),
        toggleLoader: jest.fn(),
        handleUploadFile: jest.fn(() => Promise.resolve(defaultResponse)),
        isClickUpload: true,
        file: { name: 'abc.xlsx' }
    }

    it('render correctly', () => {
        const result = shallow(<UploadResult {...defaultProps} />);
        result.setState({
            result: { isOK: true, message: 'Your file has been uploaded successfully!' },
            responseData: null
        });

        expect(result).toMatchSnapshot();
    });

    it('render correctly with selected file', () => {
        const result = shallow(<UploadResult {...defaultProps} />);
        result.setState({
            result: { isOK: false, message: 'Has error' },
            responseData: [
                {
                    Username: 'ABC',
                    Amount: 123,
                    Reason: 'This username is not exist.'
                }
            ]
        });

        expect(result).toMatchSnapshot();
    });

    it('componentDidMount - should call handleUploadFile', () => {
        const wrapper = shallow(<UploadResult {...defaultProps} />);
        const instance = wrapper.instance();

        instance.handleUploadFile = jest.fn();
        instance.componentDidMount();

        expect(instance.handleUploadFile).toHaveBeenCalled();
    });

    it('componentDidUpdate - should call handleUploadFile', () => {
        const props = Object.assign({}, defaultProps, { isClickUpload: false });

        const wrapper = shallow(<UploadResult {...defaultProps} />);
        const instance = wrapper.instance();

        instance.handleUploadFile = jest.fn();
        instance.componentDidUpdate(props);

        expect(instance.handleUploadFile).toHaveBeenCalled();
    });

    it('componentDidUpdate - should not call handleUploadFile', () => {
        const wrapper = shallow(<UploadResult {...defaultProps} />);
        const instance = wrapper.instance();

        instance.handleUploadFile = jest.fn();
        instance.componentDidUpdate(defaultProps);

        expect(instance.handleUploadFile).not.toHaveBeenCalled();
    });

    it('handleUploadFile - should upload successfully', async () => {
        const response = {
            Result: {
                IsSuccessful: true,
                Message: '',
            },
            ErroredAdjustmentInfoList: null
        }
        const props = {
            setIsClickUpload: jest.fn(),
            setIsShowResult: jest.fn(),
            toggleLoader: jest.fn(),
            handleUploadFile: jest.fn(() => Promise.resolve(response)),
            isClickUpload: true,
            file: { name: 'abc.xlsx' }
        }
        const instance = new UploadResult(props);

        window.setTimeout = jest.fn(fn => fn && fn());
        instance.setState = jest.fn();
        instance.handleRefreshPage = jest.fn();

        await instance.handleUploadFile();

        expect(instance.handleRefreshPage).toHaveBeenCalled();
        expect(instance.setState).toHaveBeenCalledWith({ responseData: null, result: { isOK: true, message: "Your file has been uploaded successfully!" } });

        expect(props.toggleLoader).toHaveBeenCalledTimes(2);
        expect(props.toggleLoader.mock.calls[0][0]).toBeTruthy();
        expect(props.toggleLoader.mock.calls[1][0]).toBeFalsy();
        expect(props.setIsShowResult).toHaveBeenCalledTimes(1);
        expect(props.setIsShowResult.mock.calls[0][0]).toBeTruthy();
        expect(props.setIsClickUpload).toHaveBeenCalledWith(false);
        expect(props.handleUploadFile).toHaveBeenCalledWith(props.file);
    });

    it('handleUploadFile - should uploading is failed', async () => {
        const response = {
            Result: {
                IsSuccessful: false,
                Message: 'Failed!',
            },
            ErroredAdjustmentInfoList: [{ UserName: "ABC" }]
        }
        const props = {
            setIsClickUpload: jest.fn(),
            setIsShowResult: jest.fn(),
            toggleLoader: jest.fn(),
            handleUploadFile: jest.fn(() => Promise.resolve(response)),
            isClickUpload: true,
            file: { name: 'abc.xlsx' }
        }
        const instance = new UploadResult(props);

        window.setTimeout = jest.fn(fn => fn && fn());
        instance.setState = jest.fn();
        instance.handleRefreshPage = jest.fn();

        await instance.handleUploadFile();

        expect(instance.handleRefreshPage).not.toHaveBeenCalled();
        expect(instance.setState).toHaveBeenCalledWith({ responseData: [{ UserName: "ABC" }], result: { isOK: false, message: "Failed!" } });

        expect(props.toggleLoader).toHaveBeenCalledTimes(2);
        expect(props.toggleLoader.mock.calls[0][0]).toBeTruthy();
        expect(props.toggleLoader.mock.calls[1][0]).toBeFalsy();
        expect(props.setIsShowResult).toHaveBeenCalledTimes(1);
        expect(props.setIsShowResult.mock.calls[0][0]).toBeTruthy();
        expect(props.setIsClickUpload).toHaveBeenCalledWith(false);
        expect(props.handleUploadFile).toHaveBeenCalledWith(props.file);
    });

    it('handleUploadFile - should not call props.handleUploadFile', async () => {
        const props = {
            setIsClickUpload: jest.fn(),
            setIsShowResult: jest.fn(),
            toggleLoader: jest.fn(),
            handleUploadFile: jest.fn(),
            isClickUpload: true,
            file: { name: 'abc.png' }
        }
        const instance = new UploadResult(props);

        window.setTimeout = jest.fn(fn => fn && fn());
        instance.setState = jest.fn();
        instance.handleRefreshPage = jest.fn();

        await instance.handleUploadFile();

        expect(instance.handleRefreshPage).not.toHaveBeenCalled();
        expect(instance.setState).toHaveBeenCalledWith({
            responseData: null,
            result: {
                isOK: false,
                message: incorrectFormatMessage
            }
        });

        expect(props.toggleLoader).toHaveBeenCalledTimes(2);
        expect(props.toggleLoader.mock.calls[0][0]).toBeTruthy();
        expect(props.toggleLoader.mock.calls[1][0]).toBeFalsy();
        expect(props.setIsShowResult).toHaveBeenCalledTimes(1);
        expect(props.setIsShowResult.mock.calls[0][0]).toBeTruthy();
        expect(props.setIsClickUpload).toHaveBeenCalledWith(false);
        expect(props.handleUploadFile).not.toHaveBeenCalled();
    });

    it('handleUploadFile - should not call props.handleUploadFile with file null', async () => {
        const props = {
            setIsClickUpload: jest.fn(),
            setIsShowResult: jest.fn(),
            toggleLoader: jest.fn(),
            handleUploadFile: jest.fn(),
            isClickUpload: true,
            file: null
        }
        const instance = new UploadResult(props);

        window.setTimeout = jest.fn(fn => fn && fn());
        instance.setState = jest.fn();
        instance.handleRefreshPage = jest.fn();

        await instance.handleUploadFile();

        expect(instance.handleRefreshPage).not.toHaveBeenCalled();
        expect(instance.setState).toHaveBeenCalledWith({
            responseData: null,
            result: {
                isOK: false,
                message: incorrectFormatMessage
            }
        });

        expect(props.toggleLoader).toHaveBeenCalledTimes(2);
        expect(props.toggleLoader.mock.calls[0][0]).toBeTruthy();
        expect(props.toggleLoader.mock.calls[1][0]).toBeFalsy();
        expect(props.setIsShowResult).toHaveBeenCalledTimes(1);
        expect(props.setIsShowResult.mock.calls[0][0]).toBeTruthy();
        expect(props.setIsClickUpload).toHaveBeenCalledWith(false);
        expect(props.handleUploadFile).not.toHaveBeenCalled();
    });

    it('handleRefreshPage - should call props', () => {
        const wrapper = shallow(<UploadResult {...defaultProps} />);
        const instance = wrapper.instance();

        instance.handleRefreshPage();

        expect(defaultProps.setFile).toHaveBeenCalledWith(null);
        expect(defaultProps.setIsShowResult).toHaveBeenCalledWith(false);
    });
})