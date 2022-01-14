import React from 'react';
import { shallow } from 'enzyme';
import Filter from '../../Filter';
import configureMockStore from 'redux-mock-store';
import { SET_IS_FROM_ANOTHER_PAGE } from '../../../actions/types';

const mockStore = configureMockStore();

describe('offer aff - components - Content - connect', () => {
    let wrapper, store;

    beforeEach(() => {
        const initialState = {
            isFromAnotherPage: true
        };
        store = mockStore(initialState);
        wrapper = shallow(<Filter store={store} />).dive();
    })

    it('should map correct value', () => {
        expect(wrapper.props().isFromAnotherPage).toBeTruthy();
    })

    it('should call correct action', () => {
        wrapper.props().setIsFromAnotherPage(false);

        const actions = store.getActions();
        expect(actions).toEqual([{ type: SET_IS_FROM_ANOTHER_PAGE, data: false }]);
    });
})