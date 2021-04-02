import React from 'react';
import { AppContainer } from '../AppContainer';
import { shallow } from 'enzyme';

describe('new view log - components - AppContainer', () => {
    it('render correctly', () => {
        const result = shallow(<AppContainer isShowLoader={true} selectedAff={''} />);

        expect(result).toMatchSnapshot();
    });

    it('render correctly with detail content', () => {
        const result = shallow(<AppContainer isShowLoader={true} selectedAff={'12BetAff001'} />);

        expect(result).toMatchSnapshot();
    });
});