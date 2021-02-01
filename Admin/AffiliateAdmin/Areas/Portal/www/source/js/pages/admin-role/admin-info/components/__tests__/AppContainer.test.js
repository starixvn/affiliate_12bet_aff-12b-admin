import React from 'react';
import { AppContainer } from '../AppContainer';
import { shallow } from 'enzyme';

describe('affiliate info - components - AppContainer', () => {
    it('render correctly', () => {
        const result = shallow(<AppContainer isShowLoader={true} userName={''} />);

        expect(result).toMatchSnapshot();
    });

    it('render correctly with has Username', () => {
        const result = shallow(<AppContainer isShowLoader={true} userName={'Abc'} />);

        expect(result).toMatchSnapshot();
    });
});