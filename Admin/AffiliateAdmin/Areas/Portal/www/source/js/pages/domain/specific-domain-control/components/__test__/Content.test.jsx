import React from 'react';
import { Content } from '../Content';
import { shallow } from 'enzyme';
import NotificationPopup from '../../../../common/components/NotificationPopup';

describe('specific domain control - components - Content ', () => {
    const gridData = [
        {
            DomainControlId: 1,
            AffUsername: 'testuser',
            AffDomain: 'test.com',
            RedirectDomain: 'redirect.com',
            Creator: 'creator',
            CreatedDate: '2024-01-01',
            IsUsed: true,
            Editor: 'editor',
            UpdatedDate: '2024-01-02'
        },
        {
            DomainControlId: 2,
            AffUsername: 'testuser2',
            AffDomain: 'test2.com',
            RedirectDomain: 'redirect2.com',
            Creator: 'creator2',
            CreatedDate: '2024-01-02',
            IsUsed: false,
            Editor: 'editor2',
            UpdatedDate: '2024-01-03'
        }
    ];

    const defaultProps = {
        shouldResetScrollPosition: true,
        gridData,
        param: 'test',
        fetchData: jest.fn(),
        deleteDomain: jest.fn(),
        toggleLoader: jest.fn(),
        setSelectedDomainId: jest.fn(),
        setShouldResetScrollPosition: jest.fn()
    };

    it('render correctly', () => {
        const result = shallow(<Content {...defaultProps} />);
        result.setState({ message: 'has NotificationPopup' });

        expect(result).toMatchSnapshot();
    });

    it('render correctly with gridData is null', () => {
        const props = Object.assign({}, defaultProps, { gridData: null });
        const result = shallow(<Content {...props} />);

        expect(result).toMatchSnapshot();
    });

    it('render correctly with gridData is empty', () => {
        const props = Object.assign({}, defaultProps, { gridData: [] });
        const result = shallow(<Content {...props} />);

        expect(result).toMatchSnapshot();
    });

    it('_handleCloseNotificationPopup - should be called when close NotificationPopup', () => {
        const result = shallow(<Content {...defaultProps} />);
        result.setState({ message: 'has NotificationPopup' });
        result.setState = jest.fn();

        result.find(NotificationPopup).props().onClose(false);

        expect(result.setState).toHaveBeenCalledWith({ message: '' });
    });

    it('_handleCloseNotificationPopup - should call setState and _deleteDomain', () => {
        const result = shallow(<Content {...defaultProps} />);
        result.setState({ selectedDomainId: 123 });

        const instance = result.instance();
        result.setState = jest.fn();
        instance._deleteDomain = jest.fn();

        instance._handleCloseNotificationPopup(true);

        expect(result.setState).toHaveBeenCalledWith({ message: '' });
        expect(instance._deleteDomain).toHaveBeenCalledWith(123);
    });

    it('_handleDeleteDomain - should be called when click delete button', () => {
        const props = Object.assign({}, defaultProps, { setShouldResetScrollPosition: jest.fn() });

        const wrapper = shallow(<Content {...props} />);
        wrapper.setState = jest.fn();
        const dataRow = shallow(<div>{wrapper.instance()._renderDataSection(gridData)}</div>);

        dataRow.find('Button').at(1).simulate('click');

        expect(props.setShouldResetScrollPosition).toHaveBeenCalledWith(false);
        expect(wrapper.setState).toHaveBeenCalledWith({ 
            message: `Are you sure you want to delete this domain setting?`, 
            isConfirmPopup: true, 
            selectedDomainId: 1 
        });
    });

    it('_handleEditDomain - should be called when click edit button', () => {
        const props = Object.assign({}, defaultProps, { 
            setShouldResetScrollPosition: jest.fn(), 
            setSelectedDomainId: jest.fn() 
        });

        const wrapper = shallow(<Content {...props} />);
        const dataRow = shallow(<div>{wrapper.instance()._renderDataSection(gridData)}</div>);

        dataRow.find('Button').at(0).simulate('click');

        expect(props.setShouldResetScrollPosition).toHaveBeenCalledWith(false);
        expect(props.setSelectedDomainId).toHaveBeenCalledWith(1);
    });

    it('_handleEditDomain - should be called when click add button', () => {
        const props = Object.assign({}, defaultProps, { 
            setShouldResetScrollPosition: jest.fn(), 
            setSelectedDomainId: jest.fn() 
        });

        const wrapper = shallow(<Content {...props} />);

        wrapper.find('Button').at(0).simulate('click');

        expect(props.setShouldResetScrollPosition).toHaveBeenCalledWith(false);
        expect(props.setSelectedDomainId).toHaveBeenCalledWith(0);
    });

    it('_deleteDomain - should call deleteDomain with delete successfully', async () => {
        const expectedResult = { IsSuccessful: true, Message: "" };
        const props = Object.assign({}, defaultProps, { 
            toggleLoader: jest.fn(), 
            fetchData: jest.fn(), 
            deleteDomain: jest.fn(() => expectedResult) 
        });
        const wrapper = shallow(<Content {...props} />);

        await wrapper.instance()._deleteDomain(1);

        expect(props.toggleLoader).toHaveBeenCalledTimes(2);
        expect(props.toggleLoader.mock.calls[0][0]).toBe(true);
        expect(props.toggleLoader.mock.calls[1][0]).toBe(false);
        expect(props.deleteDomain).toHaveBeenCalledWith({ DomainControlId: 1 });
        expect(props.fetchData).toHaveBeenCalledWith('test');
    });

    it('_deleteDomain - should call deleteDomain with delete is failed', async () => {
        const expectedResult = { IsSuccessful: false, Message: "Delete is failed!" };
        const props = Object.assign({}, defaultProps, { 
            toggleLoader: jest.fn(), 
            deleteDomain: jest.fn(() => expectedResult) 
        });
        const wrapper = shallow(<Content {...props} />);
        wrapper.setState = jest.fn();

        await wrapper.instance()._deleteDomain(1);

        expect(props.toggleLoader).toHaveBeenCalledTimes(2);
        expect(props.toggleLoader.mock.calls[0][0]).toBe(true);
        expect(props.toggleLoader.mock.calls[1][0]).toBe(false);
        expect(props.deleteDomain).toHaveBeenCalledWith({ DomainControlId: 1 });
        expect(wrapper.setState).toHaveBeenCalledWith({ 
            message: "Delete is failed!", 
            isConfirmPopup: false 
        });
    });

    it('_deleteDomain - should call deleteDomain with error', async () => {
        const props = Object.assign({}, defaultProps, { 
            toggleLoader: jest.fn(), 
            deleteDomain: jest.fn() 
        });
        const wrapper = shallow(<Content {...props} />);
        wrapper.setState = jest.fn();

        await wrapper.instance()._deleteDomain(1);

        expect(props.toggleLoader).toHaveBeenCalledTimes(2);
        expect(props.toggleLoader.mock.calls[0][0]).toBe(true);
        expect(props.toggleLoader.mock.calls[1][0]).toBe(false);
        expect(props.deleteDomain).toHaveBeenCalledWith({ DomainControlId: 1 });
        expect(wrapper.setState).toHaveBeenCalledWith({"isConfirmPopup": false, "message": "An error occurred while deleting the domain"});
    });
}); 