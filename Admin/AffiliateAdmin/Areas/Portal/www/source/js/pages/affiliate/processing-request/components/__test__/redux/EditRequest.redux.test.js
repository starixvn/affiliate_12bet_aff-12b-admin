import React from 'react';
import EditRequest from '../../EditRequest/EditRequest';
import { shallow } from 'enzyme';
import configureMockStore from 'redux-mock-store';
import { SET_SELECTED_REQUEST_ID } from '../../../actions/types';

const mockStore = configureMockStore();

describe('processing request - components - EditRequest - connect', () => {
    let wrapper, store;

    beforeEach(() => {
        const initialState = {
            languageList: [],
            selectedRequest: {},
            originalData: {},
            param: {},
            selectedRequestId: 123,
            pagingOption: {}
        };
        store = mockStore(initialState);
        wrapper = shallow(<EditRequest store={store} />).dive();
    });

    it('should show correct value', () => {
        const { selectedRequest, selectedRequestId, languageList, originalData, pagingOption, param } = wrapper.props();
        expect(selectedRequest).toEqual({});
        expect(languageList).toEqual([]);
        expect(originalData).toEqual({});
        expect(param).toEqual({});
        expect(selectedRequestId).toBe(123);
        expect(pagingOption).toEqual({});
    });

    it('should call correct action', () => {
        wrapper.props().setSelectedRequestId('123');

        const actions = store.getActions();
        expect(actions).toEqual([{ type: SET_SELECTED_REQUEST_ID, data: '123' }]);
    });
})