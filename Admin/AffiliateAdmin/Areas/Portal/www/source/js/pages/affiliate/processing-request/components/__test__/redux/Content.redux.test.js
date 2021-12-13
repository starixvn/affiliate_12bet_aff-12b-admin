import React from 'react';
import Content from '../../Content';
import { shallow } from 'enzyme';
import configureMockStore from 'redux-mock-store';
import { SET_SELECTED_REQUEST_ID } from '../../../actions/types';

const mockStore = configureMockStore();

describe('processing request - components - Content - connect', () => {
    let wrapper, store;

    beforeEach(() => {
        const initialState = {
            reportTitle: '',
            param: {},
            claimsPrincipal: { hasWriteClaim: jest.fn(() => true), hasClaim: jest.fn(() => true) },
            isClickSearch: false,
            dataSummary: {},
            selectedRequestId: 123,
            pagingOption: {}
        };
        store = mockStore(initialState);
        wrapper = shallow(<Content store={store} />).dive();
    });

    it('should show correct value', () => {
        const { reportTitle, param, isDisplayExportExcelBtn, isModifiable, isClickSearch, dataSummary, selectedRequestId, pagingOption } = wrapper.props();
        expect(reportTitle).toEqual('');
        expect(isDisplayExportExcelBtn).toBeTruthy();
        expect(isModifiable).toBeTruthy();
        expect(isClickSearch).toBeFalsy();
        expect(dataSummary).toEqual({});
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