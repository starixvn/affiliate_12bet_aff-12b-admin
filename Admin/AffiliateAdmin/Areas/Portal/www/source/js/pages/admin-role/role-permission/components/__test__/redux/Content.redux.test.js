import React from 'react';
import Content from '../../Content';
import { shallow } from 'enzyme';
import configureMockStore from 'redux-mock-store';
import { SET_IS_GENERAL } from '../../../actions/types'

const mockStore = configureMockStore();

describe('role permission - components - Content - connect', () => {
    let wrapper;
    let store;

    beforeEach(() => {
        const initialState = {
            isGeneral: true
        };
        store = mockStore(initialState);
        wrapper = shallow(<Content store={store} />).dive();
    });

    it('should show correct value', () => {
        expect(wrapper.props().isGeneral).toBeTruthy();
    });

    it('should call correct action', () => {
        wrapper.props().setIsGeneral(false);

        const actions = store.getActions();
        expect(actions).toEqual([{ type: SET_IS_GENERAL, data: false }]);
    });
})