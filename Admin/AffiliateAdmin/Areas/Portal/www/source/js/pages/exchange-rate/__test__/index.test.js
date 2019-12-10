import React from 'react';
import { shallow } from 'enzyme';
import App from '../index';

jest.mock('react-dom', () => ({ render: jest.fn() }));

describe('exchange rate - App', () => {
    it('should render correctly', () => {
        const wrapper = shallow(<App />);

        expect(wrapper).toMatchSnapshot();
    })
})