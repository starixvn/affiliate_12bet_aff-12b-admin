import React from 'react';
import { Content } from '../Content';
import { shallow, mount } from 'enzyme';
import { act } from 'react-dom/test-utils';

import { defaultPagingOption } from '../../../../../modules/constants';
import { Table } from '@9softstudio/react-fixed-table-header';
import { NotificationPopup } from '../../../../common/components/NotificationPopup';

describe('member list - components - Content ', () => {

    beforeAll(() => {
        jest.spyOn(console, 'error').mockImplementation((...args) => {
            if (!args[0].includes('Warning: An update to %s inside a test was not wrapped in act')) {
                consoleError(...args);
            }
        });
    });

    const gridData = [
        {
            AffName: "12BETAFF017",
            CreatedDate: "2017-07-06T10:24:00",
            CreatedDateText: "07/06/2017 10:24:00",
            CurrentBalance: 0,
            FirstName: "",
            IbcUserName: "12BETRMB0239317",
            LastName: "",
            MemberId: 26522982,
            UserName: "12Bet$wjpwjp"
        },
        {
            AffName: "12BETAFF177",
            CreatedDate: "2017-07-05T18:25:00",
            CreatedDateText: "07/05/2017 18:25:00",
            CurrentBalance: 0,
            FirstName: "",
            IbcUserName: "12BETRMB0239312",
            LastName: "",
            MemberId: 26518302,
            UserName: "12Bet$jingguoan"
        }
    ];

    const param = {
        AffName: "",
        UserName: "",
        IbcUserName: "",
        PageIndex: 1,
        PageSize: 20,
    };

    const defaultProps = {
        pagingOption: defaultPagingOption,
        shouldResetScrollPosition: true,
        param,
        gridData,
        fetchData: jest.fn()
    };

    it('render correctly', () => {
        const result = shallow(<Content {...defaultProps} />);

        expect(result).toMatchSnapshot();
    });

    it('render correctly with NotificationPopup', () => {
        const props = Object.assign({}, defaultProps, { fetchData: jest.fn(), setShouldResetScrollPosition: jest.fn() });

        const wrapper = mount(<Content {...props} />);

        wrapper.find('Button').at(0).simulate('click');

        expect(wrapper).toMatchSnapshot();
    });

    it('render correctly with gridData is null', () => {
        const props = Object.assign({}, defaultProps, { gridData: null })
        const result = shallow(<Content {...props} />);

        expect(result).toMatchSnapshot();
    });

    it('render correctly with gridData is empty', () => {
        const props = Object.assign({}, defaultProps, { gridData: [] })
        const result = shallow(<Content {...props} />);

        expect(result).toMatchSnapshot();
    });

    it('_handlePaging - should be called when onPaging in Table', () => {
        const props = Object.assign({}, defaultProps, { setPagingOption: jest.fn(), setParam: jest.fn() });
        const result = shallow(<Content {...props} />);

        result.find(Table).props().onPaging(50, 50);

        expect(props.setPagingOption).toHaveBeenCalledWith({ PageIndex: 50, PageSize: 50 });
        expect(props.setParam).toHaveBeenCalledWith({ PageIndex: 50, PageSize: 50, AffName: '', UserName: '', IbcUserName: '' });
    });

    it('_handleDeleteMember - should be called when click remove button', () => {
        const props = Object.assign({}, defaultProps, { fetchData: jest.fn(), setShouldResetScrollPosition: jest.fn() });

        const wrapper = mount(<Content {...props} />);

        wrapper.find('Button').at(0).simulate('click');

        expect(props.setShouldResetScrollPosition).toHaveBeenCalledWith(false);
    });

    it('_handleCloseNotificationPopup - should be called when call onClose with false in NotificationPopup', () => {
        const expectedResult = { IsSuccessful: true, Message: "" };
        const props = Object.assign({}, defaultProps, { fetchData: jest.fn(), setShouldResetScrollPosition: jest.fn(), toggleLoader: jest.fn(), deleteMember: jest.fn(() => expectedResult) });

        const wrapper = mount(<Content {...props} />);

        wrapper.find('Button').at(0).simulate('click');

        act(() => {
            wrapper.find(NotificationPopup).props().onClose(false);
        });

        expect(props.setShouldResetScrollPosition).toHaveBeenCalledWith(false);
        expect(props.toggleLoader).not.toHaveBeenCalled();
        expect(props.deleteMember).not.toHaveBeenCalled();
    });

    it('_deleteMember - should call deleteMember with delete successfully', () => {
        const expectedResult = { IsSuccessful: true, Message: "" };
        const props = Object.assign({}, defaultProps, { fetchData: jest.fn(), setShouldResetScrollPosition: jest.fn(), toggleLoader: jest.fn(), deleteMember: jest.fn(() => expectedResult) });

        const wrapper = mount(<Content {...props} />);

        wrapper.find('Button').at(0).simulate('click');

        act(() => {
            wrapper.find(NotificationPopup).props().onClose(true);
        });

        expect(props.setShouldResetScrollPosition).toHaveBeenCalledWith(false);
        expect(props.toggleLoader).toHaveBeenCalledWith(true);
        expect(props.deleteMember).toHaveBeenCalledWith(26522982);
    });

    it('_deleteMember - should call deleteMember with delete is failed', () => {
        const expectedResult = { IsSuccessful: false, Message: "Delete is failed!" };
        const props = Object.assign({}, defaultProps, { fetchData: jest.fn(), setShouldResetScrollPosition: jest.fn(), toggleLoader: jest.fn(), deleteMember: jest.fn(() => expectedResult) });

        const wrapper = mount(<Content {...props} />);

        wrapper.find('Button').at(0).simulate('click');

        act(() => {
            wrapper.find(NotificationPopup).props().onClose(true);
        });

        expect(props.setShouldResetScrollPosition).toHaveBeenCalledWith(false);
        expect(props.toggleLoader).toHaveBeenCalledWith(true);
        expect(props.deleteMember).toHaveBeenCalledWith(26522982);
    });

    it('_deleteMember - should call deleteMember with error', () => {
        const props = Object.assign({}, defaultProps, { fetchData: jest.fn(), setShouldResetScrollPosition: jest.fn(), toggleLoader: jest.fn(), deleteMember: jest.fn() });

        const wrapper = mount(<Content {...props} />);

        wrapper.find('Button').at(0).simulate('click');

        act(() => {
            wrapper.find(NotificationPopup).props().onClose(true);
        });

        expect(props.setShouldResetScrollPosition).toHaveBeenCalledWith(false);
        expect(props.toggleLoader).toHaveBeenCalledWith(true);
        expect(props.deleteMember).toHaveBeenCalledWith(26522982);
    });
})