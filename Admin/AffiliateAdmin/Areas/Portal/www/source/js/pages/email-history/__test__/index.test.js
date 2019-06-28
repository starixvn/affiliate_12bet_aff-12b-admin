import React from 'react';
import ReactDOM from 'react-dom';
import App from '../index';
import { shallow } from 'enzyme';

jest.mock('react-dom', ()=> ({render: jest.fn()}))

describe('email history - App', () => {
    it('Should render correctly', () => {
        const wrapper = shallow(<App />);

        expect(wrapper).toMatchSnapshot();
    });
})