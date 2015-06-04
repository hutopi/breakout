function save() {
    var data = {
        bricks: grid.export(),
        background: background.export(),
        music: music.export()
    }

    var result = JSON.stringify(data);
    var compressed = pako.deflate(result, {level: 9});
    saveAs(new Blob([compressed], {type: 'application/octet-stream'}), 'level.json');
}

function load() {
    var reader = new FileReader;
    reader.onloadend = function() {
        var data = JSON.parse(pako.inflate(reader.result, {to: 'string'}));
        grid.import(data.bricks);
        background.setState({
            type: data.background.type,
            file: data.background.file
        });

        music.setState({
            type: data.music.type,
            file: data.music.file
        })
    };

    var file = $('#toload')[0].files[0]
    reader.readAsBinaryString(file);
}

$('#save').click(save);
$('#load').click(load);