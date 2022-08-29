import React from 'react'
import Content from '../../Content'
import { shallow } from 'enzyme'
import configureMockStore from 'redux-mock-store'
import { TOGGLE_LOADER } from '../../../../../../actions/actiontypes';

const mockStore = configureMockStore()

describe('feedback report- Content - connect', () => {
    let wrapper, store

    beforeEach(() => {
        const initialState = {
            param: {},
            reportTitle: ''
        }
        store = mockStore(initialState)
        wrapper = shallow(<Content store={store} />).dive()
    })

    it('should show correct value', () => {
        const { param, reportTitle } = wrapper.props();
        expect(param).toEqual({})
        expect(reportTitle).toEqual('')
    })

    it('should call correct action', () => {
        wrapper.props().toggleLoader(true)

        const actions = store.getActions();
        expect(actions).toEqual([{ type: TOGGLE_LOADER, data: true }])
    })
})