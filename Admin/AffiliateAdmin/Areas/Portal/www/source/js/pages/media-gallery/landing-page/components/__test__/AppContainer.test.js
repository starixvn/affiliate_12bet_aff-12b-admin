import React from 'react';
import { AppContainer } from '../AppContainer';
import { shallow } from 'enzyme';
import ControlButtons from '../../../common/components/ControlButtons';

describe('landing page list - components - AppContainer', () => {
    it('render correctly', () => {
        const result = shallow(<AppContainer isShowLoader={true} />);

        expect(result).toMatchSnapshot();
    });
});