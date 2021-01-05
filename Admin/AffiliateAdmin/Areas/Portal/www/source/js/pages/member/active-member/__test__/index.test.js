import React from 'react';
import App from '../index';
import { shallow } from 'enzyme';

jest.mock('react-dom', () => ({ render: jest.fn() }))

describe('active member - App', () => {
    it('Should render correctly', () => {
        window.globalVariables = jest.fn();
        window.initialState = jest.fn();
        const wrapper = shallow(<App />);

        expect(wrapper).toMatchSnapshot();
    });
})