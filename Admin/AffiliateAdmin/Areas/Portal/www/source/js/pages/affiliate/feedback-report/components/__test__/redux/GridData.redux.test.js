import React from 'react';
import GridData from '../../GridData';
import { shallow } from 'enzyme';
import configureMockStore from 'redux-mock-store';
import { TOGGLE_LOADER } from '../../../../../../actions/actiontypes';

const mockStore = configureMockStore();

describe('feedback report - components - GridData - connect', () => {
    let wrapper, store;

    beforeEach(() => {
        const initialState = {
            param: {},
            pagingOption: {}
        };
        store = mockStore(initialState);
        wrapper = shallow(<GridData store={store} />).dive();
    });

    it('should show correct value', () => {
        const { param, pagingOption } = wrapper.props();

        expect(param).toEqual({});
        expect(pagingOption).toEqual({});
    });

    it('should call correct action', () => {
        wrapper.props().toggleLoader(true);

        const actions = store.getActions();
        expect(actions).toEqual([{ type: TOGGLE_LOADER, data: true }]);
    });
})