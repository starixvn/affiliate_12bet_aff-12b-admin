import React from 'react';
import ShallowRenderer from 'react-test-renderer/shallow';
import { AppContainer } from '../AppContainer';

describe('affiliate info - components - AppContainer', () => {
    it('render correctly', () => {
        const renderer = new ShallowRenderer();

        const result = renderer.render(<AppContainer isShowLoader={true} />);

        expect(result).toMatchSnapshot();
    });
});