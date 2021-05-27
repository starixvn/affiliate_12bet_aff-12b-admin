import React from 'react';
import Actived from '../../Actived';
import { shallow } from 'enzyme';
import configureMockStore from 'redux-mock-store';

const mockStore = configureMockStore();

describe('active member list - Actived - connect', () => {
    let wrapper;
    let store;

    beforeEach(() => {
        const initialState = {
            isShowMemberFullname: false
        };
        store = mockStore(initialState);
        wrapper = shallow(<Actived store={store} />).dive();
    });

    it('should show correct value', () => {
        expect(wrapper.props().isShowMemberFullname).toBeFalsy();
    });
})