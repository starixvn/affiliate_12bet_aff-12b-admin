import React from 'react';
import InsertUpdateFileHtmlForm from '../../InsertUpdateFileHtmlForm';
import { shallow } from 'enzyme';
import configureMockStore from 'redux-mock-store';
import { SET_SELECTED_ID } from '../../../actions/types';

const mockStore = configureMockStore();

describe('offer page - components - InsertUpdateFileHtmlForm - connect', () => {
    let wrapper;
    let store;

    beforeEach(() => {
        const initialState = {
            selectedId: 123
        };
        store = mockStore(initialState);
        wrapper = shallow(<InsertUpdateFileHtmlForm store={store} />).dive();
    });

    it('should show correct value', () => {
        expect(wrapper.props().selectedId).toBe(123);
    });

    it('should call correct action', () => {
        wrapper.props().setSelectedId(0);

        const actions = store.getActions();
        expect(actions).toEqual([{ type: SET_SELECTED_ID, data: 0 }]);
    });
})