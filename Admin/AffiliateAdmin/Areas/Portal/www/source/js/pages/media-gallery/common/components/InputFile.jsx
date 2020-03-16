import React from 'react';
import { Button, Label, Input} from 'reactstrap';
import { globalResource } from '../../../../modules/globalResource';
import { messages } from '../libs/constants';

export class InputFile extends React.PureComponent {
    constructor(props){
        super(props);
    }

    componentDidMount(){
        this.$input = $(this.input);
        this.$input.css('cursor','pointer');
        this.$input.click(() => {
            this.$input.children('input')[0].click();
        })
    }
    
    componentWillUnmount() {
        this.$input.unbind('click');
    }

    render ()
    {
        const {className, id, files, isMultiple, handleOnChangeFiles} = this.props;

        let text = messages.NoFileChoosen;
        if (files && files.length === 1)
        {
            text = files[0].name;
        }
        if (files && files.length > 1)
        {
            text = `${files.length} files`
        }

        return(    
            <React.Fragment>
                <div className={className} ref={el => this.input = el}>
                    <Button type="button">{globalResource.ChooseFile}</Button>
                    <Label className="ml-1">{text}</Label>
                    <Input type="file"
                        name="file" 
                        id={id}
                        hidden
                        files={files}
                        multiple={isMultiple}
                        onChange={handleOnChangeFiles} accept="image/jpg, image/gif, image/png, image/jpeg" />
                </div>
            </React.Fragment>
        )
    }
}

export default (React.memo(InputFile));