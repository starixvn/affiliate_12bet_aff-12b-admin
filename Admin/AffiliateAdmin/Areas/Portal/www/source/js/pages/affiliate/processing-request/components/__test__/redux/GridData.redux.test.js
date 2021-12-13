import React from 'react';
import GridData from '../../GridData';
import { shallow } from 'enzyme';
import configureMockStore from 'redux-mock-store';
import { SET_SELECTED_REQUEST_ID } from '../../../actions/types';

const mockStore = configureMockStore();

describe('processing request - components - GridData - connect', () => {
    let wrapper, store;

    beforeEach(() => {
        const initialState = {
            today: '10/11/2021',
            gridData: [],
            pagingOption: {},
            shouldResetScrollPosition: true,
            claimsPrincipal: {},
            param: {}
        };
        store = mockStore(initialState);
        wrapper = shallow(<GridData store={store} />).dive();
    });

    it('should show correct value', () => {
        const { today, gridData, pagingOption, shouldResetScrollPosition, claimsPrincipal, param } = wrapper.props();
        expect(today).toEqual('10/11/2021');
        expect(gridData).toEqual([]);
        expect(pagingOption).toEqual({});
        expect(shouldResetScrollPosition).toBeTruthy();
        expect(claimsPrincipal).toEqual({});
        expect(param).toEqual({});
    });

    it('should call correct action', () => {
        wrapper.props().setSelectedRequestId('123');

        const actions = store.getActions();
        expect(actions).toEqual([{ type: SET_SELECTED_REQUEST_ID, data: '123' }]);
    });
})