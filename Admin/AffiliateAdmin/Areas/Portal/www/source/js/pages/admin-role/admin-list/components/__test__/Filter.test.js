import React from 'react';
import { shallow } from 'enzyme';
import { Filter } from '../Filter';

describe('admin list - components - Filter', () => {
    it('should render correctly', () => {
        const wrapper = shallow(<Filter isBack={false} />);

        expect(wrapper).toMatchSnapshot();
    })

    it("_handleSearch - should call setParam", () => {
        const props = { isBack: true, setParam: jest.fn() };
        const wrapper = shallow(<Filter {...props} />);

        wrapper.instance().componentDidMount();

        expect(props.setParam).toHaveBeenCalledWith({ userName: '' });
    });

    it("handleChange - should call setState", () => {
        const mockedEvent = { target: { name: 'userName', value: "Abc" } };
        const wrapper = shallow(<Filter isBack={false} />);
        const instance = wrapper.instance();
        instance.setState = jest.fn();

        instance._handleChange(mockedEvent);

        expect(instance.setState).toHaveBeenCalledWith({ userName: "Abc" });
    });

    it('_handlePressEnter - should call _handleSearch', () => {
        const mockedEvent = { charCode: 13, preventDefault: jest.fn() };
        const wrapper = shallow(<Filter isBack={false} />);
        const instance = wrapper.instance();
        instance._handleSearch = jest.fn();

        instance._handlePressEnter(mockedEvent);

        expect(instance._handleSearch).toHaveBeenCalled();
    });

    it('_handlePressEnter - should not call _handleSearch', () => {
        const mockedEvent = { charCode: 11 };
        const wrapper = shallow(<Filter isBack={false} />);
        const instance = wrapper.instance();
        instance._handleSearch = jest.fn();

        instance._handlePressEnter(mockedEvent);

        expect(instance._handleSearch).not.toHaveBeenCalled();
    });
});