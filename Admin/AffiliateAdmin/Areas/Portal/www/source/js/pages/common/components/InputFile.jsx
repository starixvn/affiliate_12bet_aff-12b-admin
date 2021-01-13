import React from 'react';
import { Button, Label, Input } from 'reactstrap';

export class InputFile extends React.PureComponent {
    constructor(props) {
        super(props);
    }

    componentDidMount() {
        this.$input = $(this.input);
        this.$input.css('cursor', 'pointer');
        this.$input.click(() => {
            this.$input.children('input')[0].click();
        })
    }

    componentWillUnmount() {
        this.$input.unbind('click');
    }

    render() {
        const { className, files, isMultiple, handleOnChangeFiles, accept, btnName, lblText } = this.props;

        let text = lblText;
        if (files && files.length === 1) {
            text = files[0].name;
        }
        if (files && files.length > 1) {
            text = `${files.length} files`
        }

        return (
            <React.Fragment>
                <div className={className} ref={el => this.input = el}>
                    <Button type="button">{btnName}</Button>
                    <Label className="px-2 m-0">{text}</Label>
                    <Input type="file"
                        name="file"
                        hidden
                        files={files}
                        multiple={isMultiple}
                        onChange={handleOnChangeFiles} accept={accept} />
                </div>
            </React.Fragment>
        )
    }
}

export default (React.memo(InputFile));