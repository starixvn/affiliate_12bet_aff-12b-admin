import React from 'react';
import { Content } from '../Content';
import renderer from 'react-test-renderer';

describe('affiliate info - Content ', () => {
    it('shouldComponentUpdate - return correct value if report title no change ', () => {
        const props = {
            reportTitle: '',
            param: {},
            languageList: {}
        }

        const componentInstance = new Content(props);

        const result = componentInstance.shouldComponentUpdate({reportTitle: 'New Title'});

        expect(result).toBeTruthy();
        
    });
});