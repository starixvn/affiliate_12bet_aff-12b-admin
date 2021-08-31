import React from 'react';
import App from '../index';
import { shallow } from 'enzyme';

jest.mock('react-dom', ()=> ({render: jest.fn()}))

describe('payment request - App', () => {
    it('Should render correctly', () => {
        window.globalVariables = {};
        window.initialState = { param: {} };

        const wrapper = shallow(<App />);

        expect(wrapper).toMatchSnapshot();
    });

    it('Should render correctly with IsFromHome', () => {
        window.globalVariables = {};
        window.initialState = { param: { IsFromHome: true } };

        const wrapper = shallow(<App />);

        expect(wrapper).toMatchSnapshot();
    });
})