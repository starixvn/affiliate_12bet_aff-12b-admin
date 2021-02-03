import React from 'react';
import { Content } from '../Content';
import { shallow } from 'enzyme';

describe('mass adjustment - components - Content ', () => {
    it('render correctly', () => {
        const result = shallow(<Content isShowResult={false} />);

        expect(result).toMatchSnapshot();
    });

    it('render correctly with show result', () => {
        const result = shallow(<Content isShowResult={true} />);

        expect(result).toMatchSnapshot();
    });
});