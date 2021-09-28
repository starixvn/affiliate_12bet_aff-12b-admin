import React from 'react'
import GeneralInfo from '../../GeneralInfo'
import { shallow } from 'enzyme'
import configureMockStore from 'redux-mock-store'

const mockStore = configureMockStore()

describe('customer - left panel - GeneralInfo - connect', () => {
    let wrapper, store

    beforeEach(() => {
        const initialState = {
            leftPanel: {
                affiliateInfo: {}
            },
            claimsPrincipal: {}
        }
        store = mockStore(initialState)
        wrapper = shallow(<GeneralInfo store={store} />).dive()
    })

    it('should show correct value', () => {
        expect(wrapper.props().affiliateInfo ).toEqual({})
        expect(wrapper.props().claimsPrincipal ).toEqual({})
    })
})