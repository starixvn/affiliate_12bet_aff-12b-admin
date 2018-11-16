import React from 'react';
import Enzyme, { shallow } from 'enzyme';
import renderer from 'react-test-renderer';
import Adapter from 'enzyme-adapter-react-16';
import ShallowRenderer from 'react-test-renderer/shallow';
import { Filter } from '../Filter';

jest.mock('../../modules/constants');

Enzyme.configure({ adapter: new Adapter() });

describe('afiliate info - Filter ', () => {
    const defaultProps = {
        languageList: {},
        commGroupList: {},
        param: {},
        groupId: 0
    };

    it('componentDidMount - should call handleClickSearch if initial group Id different 0', () => {
        const mockSetReportTitle = jest.fn();
        const mockSetParam = jest.fn();
        const props = Object.assign({}, defaultProps, { groupId: 123, setReportTitle: mockSetReportTitle, setParam: mockSetParam });
        const componentInstance = new Filter(props);
        componentInstance.componentDidMount();

        expect(props.setReportTitle).toHaveBeenCalled();
        expect(props.setParam).toHaveBeenCalled();
    });


    it("should call handleChange when change Username's value", () => {
        const mockedEvent = { target: {name: 'userName', value: "abc"} }

        const wrapper = shallow(<Filter {...defaultProps} />);

        wrapper.find('Input[type="text"]').simulate('change', mockedEvent);
        
        expect(wrapper.state('userName')).toEqual("abc");
    });

    it("should call handleChange when change Email's value", () => {
        const mockedEvent = { target: {name: 'email', value: "abc@gmail.com"} }

        const wrapper = shallow(<Filter {...defaultProps} />);

        wrapper.find('Input[type="text"]').simulate('change', mockedEvent);
        
        expect(wrapper.state('email')).toEqual("abc@gmail.com");
    });

    it("should call handleChange when change Search By's value", () => {
        const mockedEvent = { target: {name: 'selectedCommGroup', value: 1} }
        const wrapper = shallow(<Filter {...defaultProps} />);

        wrapper.find('Input[type="select"]').at(0).simulate('change', mockedEvent);

        expect(wrapper.state('selectedCommGroup')).toBe(1);
    });

    it("should call handleChange when change Language's value", () => {
        const mockedEvent = { target: {name: 'language', value: 'English'} }
        const wrapper = shallow(<Filter {...defaultProps} />);

        wrapper.find('Input[type="select"]').at(1).simulate('change', mockedEvent);

        expect(wrapper.state('language')).toEqual('English');
    });

    it("should call handleChange when change Status's value", () => {
        const mockedEvent = { target: {name: 'status', value: 'Username'} }
        const wrapper = shallow(<Filter {...defaultProps} />);

        wrapper.find('Input[type="select"]').at(2).simulate('change', mockedEvent);

        expect(wrapper.state('status')).toEqual('Username');
    });

    it("should call handleChange when change Sort By's value", () => {
        const mockedEvent = { target: {name: 'sortBy', value: 'Favorite'} }
        const wrapper = shallow(<Filter {...defaultProps} />);

        wrapper.find('Input[type="select"]').at(3).simulate('change', mockedEvent);

        expect(wrapper.state('sortBy')).toEqual('Favorite');
    });

    it('should call handlePressEnter when press Enter', () => {
        const mockedEvent = {charCode: 13};
        const mockSetReportTitle = jest.fn();
        const mockSetParam = jest.fn();
        const props = Object.assign({}, defaultProps, { setReportTitle: mockSetReportTitle, setParam: mockSetParam });

        const wrapper = shallow(<Filter {...props} />);
        const componentInstance = new Filter(props);
        componentInstance.handlePressEnter(mockedEvent);

        wrapper.find('Input[type="text"]').simulate('keydown', mockedEvent);

        expect(props.setReportTitle).toHaveBeenCalled();
        expect(props.setParam).toHaveBeenCalled();
    });
    
    it('should call handleChangeSortAscending when click checkbox A-Z', () => {
        const wrapper = shallow(<Filter {...defaultProps} />);

        wrapper.find('Input[type="checkbox"]').simulate('change');

        expect(wrapper.state('isSortAscending')).toEqual(false);
    });

    it('should call handleClickSearch when click search button', () => {
        const mockSetReportTitle = jest.fn();
        const mockSetParam = jest.fn();
        const props = Object.assign({}, defaultProps, { setReportTitle: mockSetReportTitle, setParam: mockSetParam });

        const wrapper = shallow(<Filter {...props} />);
        const componentInstance = new Filter(props);
        componentInstance.handleClickSearch();

        wrapper.find('Button').simulate('click');

        expect(props.setReportTitle).toHaveBeenCalled();
        expect(props.setParam).toHaveBeenCalled();
    });

    it('should render correctly', () => {
        const renderer = new ShallowRenderer();

        const result = renderer.render(<Filter {...defaultProps} />);

        expect(result).toMatchSnapshot();
    });
})