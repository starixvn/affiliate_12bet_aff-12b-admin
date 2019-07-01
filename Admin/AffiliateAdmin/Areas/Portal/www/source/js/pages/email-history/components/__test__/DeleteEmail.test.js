import React from 'react';
import { DeleteEmail } from '../DeleteEmail';
import { shallow } from 'enzyme';
import * as ajaxRequest from '../../../../modules/ajaxRequest';

describe('payment request - components - Content ', () => {
    const defaultProps = {
        checkedEmails: {},
        param: {
            FromDate: "6/28/2019",
            IsGetAll: true,
            PageIndex: 1,
            PageSize: 20,
            SearchById: 1,
            SearchText: "",
            ToDate: "6/28/2019"
        }
    }

    it('render correctly', () => {
        const result = shallow(<DeleteEmail {...defaultProps} />);

        expect(result).toMatchSnapshot();
    });

    it('handleDelete - should alert PleaseSelect when not choose', () => {
        const toggleLoader = jest.fn();
        const deleteEmailHistory = jest.fn();
        const fetchData = jest.fn();
        window.confirm = jest.fn(() => false);
        window.alert = jest.fn();
        const props = Object.assign({}, defaultProps, { toggleLoader, deleteEmailHistory, fetchData });
        const wrapper = shallow(<DeleteEmail {...props} />);

        wrapper.find('Button').simulate('click');

        expect(alert).toHaveBeenCalledWith('Please select items to delete');
        expect(window.confirm).not.toHaveBeenCalled();
        expect(props.toggleLoader).not.toHaveBeenCalled();
        expect(props.deleteEmailHistory).not.toHaveBeenCalled();
        expect(props.fetchData).not.toHaveBeenCalled();
    });

    it('handleDelete - should not call everything when have choose and confirm false', () => {
        const toggleLoader = jest.fn();
        const deleteEmailHistory = jest.fn();
        const fetchData = jest.fn();
        window.confirm = jest.fn(() => false);
        window.alert = jest.fn();
        const props = Object.assign({}, defaultProps, { toggleLoader, deleteEmailHistory, fetchData, checkedEmails: { 2205: false, 2255: true } });
        const wrapper = shallow(<DeleteEmail {...props} />);

        wrapper.find('Button').simulate('click');

        expect(alert).not.toHaveBeenCalled();
        expect(window.confirm).toHaveBeenCalledWith('Are you sure to delete the selected emails?');
        expect(props.toggleLoader).not.toHaveBeenCalled();
        expect(props.deleteEmailHistory).not.toHaveBeenCalled();
        expect(props.fetchData).not.toHaveBeenCalled();
    });

    it('handleDelete - should call toggleLoader, deleteEmailHistory, alert and fetchData with correct parameters', async () => {
        const data = { IsSuccessful: true, Message: '' };

        const toggleLoader = jest.fn();
        const deleteEmailHistory = jest.fn(() => data);
        const fetchData = jest.fn();
        window.confirm = jest.fn(() => true);
        window.alert = jest.fn();
        const props = Object.assign({}, defaultProps, { toggleLoader, deleteEmailHistory, fetchData, checkedEmails: { 2205: false, 2255: true } });
        const wrapper = shallow(<DeleteEmail {...props} />);

        await wrapper.find('Button').simulate('click');

        expect(props.toggleLoader.mock.calls[0][0]).toBe(true);
        expect(window.confirm).toHaveBeenCalledWith('Are you sure to delete the selected emails?');
        expect(props.deleteEmailHistory).toHaveBeenCalledWith({ EmailIdList: "2255" });
        expect(alert).toHaveBeenCalledWith('Delete Emails Successfully');
        expect(props.fetchData).toHaveBeenCalledWith(defaultProps.param);
        expect(props.toggleLoader.mock.calls[1][0]).toBe(false);
    });

    it('handleDelete - should call toggleLoader, deleteEmailHistory and alert but not call fetchData with IsSuccessful is false', async () => {
        const data = { IsSuccessful: false, Message: 'Delete is failed!' };

        const toggleLoader = jest.fn();
        const deleteEmailHistory = jest.fn(() => data);
        const fetchData = jest.fn();
        window.confirm = jest.fn(() => true);
        window.alert = jest.fn();
        const props = Object.assign({}, defaultProps, { toggleLoader, deleteEmailHistory, fetchData, checkedEmails: { 2205: false, 2255: true } });
        const wrapper = shallow(<DeleteEmail {...props} />);

        await wrapper.find('Button').simulate('click');

        expect(props.toggleLoader.mock.calls[0][0]).toBe(true);
        expect(window.confirm).toHaveBeenCalledWith('Are you sure to delete the selected emails?');
        expect(props.deleteEmailHistory).toHaveBeenCalledWith({ EmailIdList: "2255" });
        expect(props.fetchData).not.toHaveBeenCalled();
        expect(alert).toHaveBeenCalledWith('Delete is failed!');
        expect(props.toggleLoader.mock.calls[1][0]).toBe(false);
    });

    it('handleDelete - should call toggleLoader and deleteEmailHistory but not call alert and fetchData with response is error', async () => {
        ajaxRequest.post = jest.fn();
        const toggleLoader = jest.fn();
        const deleteEmailHistory = jest.fn();
        const fetchData = jest.fn();
        window.confirm = jest.fn(() => true);
        window.alert = jest.fn();
        const props = Object.assign({}, defaultProps, { toggleLoader, deleteEmailHistory, fetchData, checkedEmails: { 2205: false, 2255: true } });
        const wrapper = shallow(<DeleteEmail {...props} />);

        await wrapper.find('Button').simulate('click');

        expect(props.toggleLoader.mock.calls[0][0]).toBe(true);
        expect(window.confirm).toHaveBeenCalledWith('Are you sure to delete the selected emails?');
        expect(props.deleteEmailHistory).toHaveBeenCalledWith({ EmailIdList: "2255" });
        expect(props.fetchData).not.toHaveBeenCalled();
        expect(window.alert).not.toHaveBeenCalled();
        expect(props.toggleLoader.mock.calls[1][0]).toBe(false);
    });
})