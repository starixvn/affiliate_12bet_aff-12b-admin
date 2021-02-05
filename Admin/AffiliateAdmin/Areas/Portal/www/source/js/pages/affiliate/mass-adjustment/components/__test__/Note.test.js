import React from 'react';
import Note from '../Note';
import { shallow } from 'enzyme';

describe('mass adjustment - components - Note ', () => {

    it('render correctly', () => {
        const result = shallow(<Note />);

        expect(result).toMatchSnapshot();
    });
})