import React from 'react';
import { shallow } from 'enzyme';
import { Filter } from '../Filter';

describe('commission group - Filter', () => {
    const defaultProps = {
        pagingOption: {},
        searchedGroupName: '',
        commGroupTypes: []
    };

    it("should call getGroupSettingList when click button Search", () => {
        const mockGetGroupSettingList = jest.fn();
        const mockSetSearchedGroupName = jest.fn();
        const mockSetSearchedGroupType = jest.fn();

        const expectParam = {
            PageIndex: 1,
            PageSize: 20,
            GroupName: 'abc',
            GroupTypeId: 1
        }
        const props = Object.assign({}, defaultProps, {
            getGroupSettingList: mockGetGroupSettingList,
            setSearchedGroupName: mockSetSearchedGroupName,
            setSearchedGroupType: mockSetSearchedGroupType,
            pagingOption: { PageIndex: 1, PageSize: 20 }
        });
        const wrapper = shallow(<Filter {...props} />);
        wrapper.setState({ groupName: 'abc', groupTypeId: 1 });
        wrapper.instance().handleSearch = jest.fn();

        wrapper.find('Button').simulate('click');

        expect(props.getGroupSettingList).toHaveBeenCalledWith(expectParam);
    });

    it("should call handleChange when change Group Name's value", () => {
        const mockedEvent = { target: { name: 'groupName', value: "Group name" } };
        const wrapper = shallow(<Filter {...defaultProps} />);
        wrapper.setState = jest.fn();

        wrapper.find('Input[type="text"]').simulate('change', mockedEvent);

        expect(wrapper.setState).toHaveBeenCalledWith({ groupName: "Group name" });
    });

    it('handlePressEnter - should call handleSearch when press Enter', () => {

        const mockedEvent = { charCode: 13, preventDefault: jest.fn() };
        const mockGetGroupSettingList = jest.fn();

        const props = Object.assign({}, defaultProps, { getGroupSettingList: mockGetGroupSettingList });

        const wrapper = shallow(<Filter {...props} />);
        const instance = wrapper.instance();
        instance.handleSearch = jest.fn();

        wrapper.find('Input[type="text"]').simulate('keyPress', mockedEvent);

        expect(instance.handleSearch).toHaveBeenCalled();
    });

    it('handlePressEnter - should not call handleSearch when press keyboard different Enter', () => {

        const mockedEvent = { charCode: 11 };
        const mockGetGroupSettingList = jest.fn();

        const props = Object.assign({}, defaultProps, { getGroupSettingList: mockGetGroupSettingList });

        const wrapper = shallow(<Filter {...props} />);
        const instance = wrapper.instance();
        instance.handleSearch = jest.fn();

        wrapper.find('Input[type="text"]').simulate('keyPress', mockedEvent);

        expect(instance.handleSearch).not.toHaveBeenCalled();
    });

    it('should render correctly', () => {
        const mockGetGroupSettingList = jest.fn();

        const props = Object.assign({}, defaultProps, { getGroupSettingList: mockGetGroupSettingList });
        const wrapper = shallow(<Filter {...props} />);

        expect(wrapper).toMatchSnapshot();
    })
});