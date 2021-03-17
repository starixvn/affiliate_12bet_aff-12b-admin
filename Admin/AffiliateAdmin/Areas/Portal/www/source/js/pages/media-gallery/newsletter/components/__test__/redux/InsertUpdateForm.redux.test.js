import React from 'react';
import InsertUpdateForm from '../../InsertUpdateForm';
import { shallow } from 'enzyme';
import configureMockStore from 'redux-mock-store';
import { SET_SELECTED_NEWSLETTER_PAGE_ID } from '../../../actions/types';

const mockStore = configureMockStore();

describe('news letter - components - InsertUpdateForm - connect', () => {
    let wrapper;
    let store;

    beforeEach(() => {
        const initialState = {
            selectedNewsLetterPageId: 123
        };
        store = mockStore(initialState);
        wrapper = shallow(<InsertUpdateForm store={store} />).dive();
    });

    it('should show correct value', () => {
        expect(wrapper.props().selectedNewsLetterPageId).toBe(123);
    });

    it('should call correct action', () => {
        wrapper.props().setSelectedNewsLetterPageId(0);

        const actions = store.getActions();
        expect(actions).toEqual([{ type: SET_SELECTED_NEWSLETTER_PAGE_ID, data: 0 }]);
    });
})