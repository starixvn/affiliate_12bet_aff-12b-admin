import React from 'react';
import { ImportForm } from '../ImportForm';
import { shallow } from 'enzyme';
import { InputFile } from '../../../../common/components/InputFile';

describe('mass adjustment - components - ImportForm ', () => {
    afterAll(() => {
        jest.resetAllMocks();
    })

    const defaultProps = {
        file: null
    }

    it('render correctly', () => {
        const result = shallow(<ImportForm {...defaultProps} />);

        expect(result).toMatchSnapshot();
    });

    it('render correctly with selected file', () => {
        const result = shallow(<ImportForm {...defaultProps} />);
        result.setState({ files: [{ name: 'abc.xlsx' }] });

        expect(result).toMatchSnapshot();
    });

    it('componentWillUnmount - should call props', () => {
        const props = Object.assign({}, defaultProps, {
            setFile: jest.fn(),
            setIsShowResult: jest.fn()
        });

        const wrapper = shallow(<ImportForm {...props} />);
        const instance = wrapper.instance();

        instance.componentWillUnmount();

        expect(props.setFile).toHaveBeenCalledWith(null);
        expect(props.setIsShowResult).toHaveBeenCalledWith(false);
    });

    it('componentDidUpdate - should call setState', () => {
        const props = Object.assign({}, defaultProps, { file: { name: 'abc.xlsx' } });

        const wrapper = shallow(<ImportForm {...defaultProps} />);
        const instance = wrapper.instance();
        instance.setState = jest.fn();

        instance.componentDidUpdate(props);

        expect(instance.setState).toHaveBeenCalledWith({ files: [] });
    });

    it('componentDidUpdate - should not call setState', () => {
        const wrapper = shallow(<ImportForm {...defaultProps} />);
        const instance = wrapper.instance();
        instance.setState = jest.fn();

        instance.componentDidUpdate(defaultProps);

        expect(instance.setState).not.toHaveBeenCalled();
    });

    it("handleChangeFile - should call setState", () => {
        const mockedEvent = { target: { files: [{ name: 'abc.xlsx' }] } };

        const wrapper = shallow(<ImportForm {...defaultProps} />);

        wrapper.setState = jest.fn();

        wrapper.find(InputFile).props().handleOnChangeFiles(mockedEvent);

        expect(wrapper.setState).toHaveBeenCalledWith({ "files": [{ name: "abc.xlsx" }] });
    });

    it("handleChangeFile - should be not call setState", () => {
        const mockedEvent = { target: { files: [] } };

        const wrapper = shallow(<ImportForm {...defaultProps} />);

        wrapper.setState = jest.fn();

        wrapper.find(InputFile).props().handleOnChangeFiles(mockedEvent);

        expect(wrapper.setState).not.toHaveBeenCalled();
    });

    it('handleClickUpload - should call props when click Upload Button', () => {
        const files = [{ name: 'abc.xlsx' }];
        const props = Object.assign({}, defaultProps, {
            setFile: jest.fn(),
            setIsClickUpload: jest.fn(),
            setIsShowResult: jest.fn()
        });
        const wrapper = shallow(<ImportForm {...props} />);
        wrapper.instance().setState({ files });

        wrapper.find('Button').simulate('click');

        expect(props.setFile).toHaveBeenCalledWith(files[0]);
        expect(props.setIsShowResult).toHaveBeenCalledWith(true);
        expect(props.setIsClickUpload).toHaveBeenCalledWith(true);
    });

    it('handleClickUpload - should be not call props', () => {
        const props = Object.assign({}, defaultProps, {
            setIsClickUpload: jest.fn()
        });
        const wrapper = shallow(<ImportForm {...props} />);

        wrapper.find('Button').simulate('click');

        expect(props.setIsClickUpload).not.toHaveBeenCalled();
    });
})