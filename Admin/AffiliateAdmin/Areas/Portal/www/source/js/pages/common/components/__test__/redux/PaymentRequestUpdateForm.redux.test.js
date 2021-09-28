import React from 'react'
import PaymentRequestUpdateForm from '../../PaymentRequestUpdateForm'
import { shallow } from 'enzyme'
import configureMockStore from 'redux-mock-store'

const mockStore = configureMockStore()

describe('common - PaymentRequestUpdateForm - connect', () => {
    let wrapper, store

    beforeEach(() => {
        const initialState = {
            claimsPrincipal: {
                hasWriteClaim: jest.fn(() => true),
                hasReadClaim: jest.fn(() => true)
            }
        }
        store = mockStore(initialState)
        wrapper = shallow(<PaymentRequestUpdateForm store={store} />).dive()
    })

    it('should show correct value', () => {
        const { isUpdatable, isShowEmail, isShowPaymentInfo } = wrapper.props();
        expect(isUpdatable).toBeTruthy()
        expect(isShowEmail).toBeTruthy()
        expect(isShowPaymentInfo).toBeTruthy()
    })
})