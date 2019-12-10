import React from 'react';
import { Content } from '../Content';
import { shallow, mount } from 'enzyme';

import { defaultPagingOption } from '../../../../../modules/constants';
import { NotificationPopup } from '../../../../common/components/NotificationPopup';

describe('member list - components - Content ', () => {

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

    it('render correctly with NotificationPopup', async () => {
        const props = Object.assign({}, defaultProps, { fetchData: jest.fn(), setShouldResetScrollPosition: jest.fn() });
        const wrapper = shallow(<Content {...props} />);
        wrapper.setState({ message: 'Has NotificationPopup' });

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

    it('componentDidUpdate - should call fetchData', () => {
        const result = shallow(<Content {...defaultProps} />);

        const props = Object.assign({}, defaultProps, { fetchData: jest.fn(), param: null });
        result.instance().componentDidUpdate(props);

        expect(defaultProps.fetchData).toHaveBeenCalledWith(param);
    });

    it('_handlePaging - should be called when onPaging in Table', () => {
        const props = Object.assign({}, defaultProps, { setPagingOption: jest.fn(), setParam: jest.fn() });
        const result = shallow(<Content {...props} />);

        result.instance()._handlePaging(50, 50);

        expect(props.setPagingOption).toHaveBeenCalledWith({ PageIndex: 50, PageSize: 50 });
        expect(props.setParam).toHaveBeenCalledWith({ PageIndex: 50, PageSize: 50, AffName: '', UserName: '', IbcUserName: '' });
    });

    it('_handleDeleteMember - should be called when click remove button', () => {
        const props = Object.assign({}, defaultProps, { fetchData: jest.fn(), setShouldResetScrollPosition: jest.fn() });

        const wrapper = shallow(<Content {...props} />);
        wrapper.setState = jest.fn();
        const dataRow = shallow(<div>{wrapper.instance()._renderDataSection(gridData)}</div>);

        dataRow.find('Button').at(0).simulate('click');

        expect(props.setShouldResetScrollPosition).toHaveBeenCalledWith(false);
        expect(wrapper.setState).toHaveBeenCalledWith({ message: 'Are you sure you want to remove this member from his affiliate?', isConfirmPopup: true, selectedMemberId: 26522982 });
    });

    it('_handleCloseNotificationPopup - should be called when call onClose with true in NotificationPopup', () => {
        const wrapper = shallow(<Content {...defaultProps} />);
        wrapper.instance()._deleteMember = jest.fn();
        wrapper.setState({ message: 'Has NotificationPopup', isConfirmPopup: true, selectedMemberId: 26522982 });

        wrapper.find(NotificationPopup).props().onClose(true);

        expect(wrapper.instance()._deleteMember).toHaveBeenCalledWith(26522982);
    });

    it('_handleCloseNotificationPopup - should be called when call onClose with false in NotificationPopup', () => {
        const wrapper = shallow(<Content {...defaultProps} />);
        wrapper.instance()._deleteMember = jest.fn();
        wrapper.setState({ message: 'Has NotificationPopup', isConfirmPopup: true, selectedMemberId: 26522982 });

        wrapper.find(NotificationPopup).props().onClose(false);

        expect(wrapper.instance()._deleteMember).not.toHaveBeenCalled();
    });

    it('_deleteMember - should call deleteMember with delete successfully', async () => {
        const expectedResult = { IsSuccessful: true, Message: "" };
        const props = Object.assign({}, defaultProps, { fetchData: jest.fn(), toggleLoader: jest.fn(), deleteMember: jest.fn(() => expectedResult) });
        const wrapper = shallow(<Content {...props} />);

        await wrapper.instance()._deleteMember(26522982);

        expect(props.toggleLoader).toHaveBeenCalledTimes(2);
        expect(props.toggleLoader.mock.calls[0][0]).toBe(true);
        expect(props.toggleLoader.mock.calls[1][0]).toBe(false);
        expect(props.deleteMember).toHaveBeenCalledWith(26522982);
        expect(props.fetchData).toHaveBeenCalledWith(param);
    });

    it('_deleteMember - should call deleteMember with delete is failed', async () => {
        const expectedResult = { IsSuccessful: false, Message: "Delete is failed!" };
        const props = Object.assign({}, defaultProps, { fetchData: jest.fn(), toggleLoader: jest.fn(), deleteMember: jest.fn(() => expectedResult) });
        const wrapper = shallow(<Content {...props} />);
        wrapper.setState = jest.fn();

        await wrapper.instance()._deleteMember(26522982);

        expect(props.toggleLoader).toHaveBeenCalledTimes(2);
        expect(props.toggleLoader.mock.calls[0][0]).toBe(true);
        expect(props.toggleLoader.mock.calls[1][0]).toBe(false);
        expect(props.deleteMember).toHaveBeenCalledWith(26522982);
        expect(wrapper.setState).toHaveBeenCalledWith({ message: "Delete is failed!", isConfirmPopup: false });
    });

    it('_deleteMember - should call deleteMember with error', async () => {
        const props = Object.assign({}, defaultProps, { toggleLoader: jest.fn(), deleteMember: jest.fn() });
        const wrapper = shallow(<Content {...props} />);
        wrapper.setState = jest.fn();

        await wrapper.instance()._deleteMember(26522982);

        expect(props.toggleLoader).toHaveBeenCalledTimes(2);
        expect(props.toggleLoader.mock.calls[0][0]).toBe(true);
        expect(props.toggleLoader.mock.calls[1][0]).toBe(false);
        expect(props.deleteMember).toHaveBeenCalledWith(26522982);
        expect(wrapper.setState).not.toHaveBeenCalled();
    });
})