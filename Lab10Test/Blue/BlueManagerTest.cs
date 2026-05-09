using lab10.blue;
using microsoft.visualstudio.testtools.unittesting;
using system.text.json;

namespace lab10test.blue
{
    [testclass]
    public sealed class bluemanagertest
    {
        private lab9.blue.blue[] _tasks;
        private string[] _input;
        private string[] _sequence;

        [testinitialize]
        public void loaddata()
        {
            var folder = directory.getparent(directory.getcurrentdirectory()).parent.parent.parent.fullname;
            var file = path.combine(folder, "lab10test", "blue", "data.json");

            var json = jsonserializer.deserialize<jsonelement>(file.readalltext(file));

            _input = json.getproperty("task2").getproperty("input").deserialize<string[]>();
            _sequence = json.getproperty("task2").getproperty("inputsequence").deserialize<string[]>();
        }

        private void init(int i)
        {
            _tasks = new lab9.blue.blue[]
            {
                 new lab9.blue.task1(_input[i]),
                 new lab9.blue.task2(_input[i], _sequence[i]),
                 new lab9.blue.task3(_input[i]),
                 new lab9.blue.task4(_input[i])
            };

            foreach (var t in _tasks)
                t.review();
        }

        [testmethod]
        public void test_00_oop()
        {
            var type = typeof(lab10.blue.blue<lab9.blue.blue>);

            assert.istrue(type.isclass, "blue<t> must be class");

            assert.isnotnull(type.getproperty("manager"));
            assert.isnotnull(type.getproperty("tasks"));

            assert.isnotnull(type.getmethod("add", new[] { typeof(lab9.blue.blue) }));
            assert.isnotnull(type.getmethod("add", new[] { typeof(lab9.blue.blue[]) }));
            assert.isnotnull(type.getmethod("remove", new[] { typeof(lab9.blue.blue) }));
            assert.isnotnull(type.getmethod("clear"));
            assert.isnotnull(type.getmethod("savetasks"));
            assert.isnotnull(type.getmethod("loadtasks"));
            assert.isnotnull(type.getmethod("changemanager"));
        }

        [testmethod]
        public void test_01_add_remove_alltasks()
        {
            for (int i = 0; i < _input.length; i++)
            {
                init(i);

                var w = new lab10.blue.blue<lab9.blue.blue>();

                w.add(_tasks);
                assert.areequal(_tasks.length, w.tasks.length, $"add failed test {i}");

                w.remove(_tasks[0]);
                assert.isfalse(w.tasks.contains(_tasks[0]), $"remove failed test {i}");
            }
        }

        [testmethod]
        public void test_02_save_load_txt()
        {
            for (int i = 0; i < _input.length; i++)
            {
                init(i);

                var manager = new bluetxtfilemanager<lab9.blue.blue>("txt");
                var folder = path.combine(path.gettemppath(), $"bluetxt_{i}");
                directory.createdirectory(folder);

                manager.selectfolder(folder);

                var w = new lab10.blue.blue<lab9.blue.blue>(manager, _tasks);

                w.savetasks();
                w.loadtasks();

                for (int j = 0; j < _tasks.length; j++)
                {
                    assert.areequal(_tasks[j].input, w.tasks[j].input,
                        $"txt load mismatch test {i} task {j}");
                }

                directory.delete(folder, true);
            }
        }

        [testmethod]
        public void test_03_save_load_json()
        {
            for (int i = 0; i < _input.length; i++)
            {
                init(i);

                var manager = new bluejsonfilemanager<lab9.blue.blue>("json");
                var folder = path.combine(path.gettemppath(), $"bluejson_{i}");
                directory.createdirectory(folder);

                manager.selectfolder(folder);

                var w = new lab10.blue.blue<lab9.blue.blue>(manager, _tasks);

                w.savetasks();
                w.loadtasks();

                for (int j = 0; j < _tasks.length; j++)
                {
                    assert.areequal(_tasks[j].input, w.tasks[j].input,
                        $"json load mismatch test {i} task {j}");
                }

                directory.delete(folder, true);
            }
        }

        [testmethod]
        public void test_04_changemanager_and_format()
        {
            for (int i = 0; i < _input.length; i++)
            {
                init(i);

                var txtmanager = new bluetxtfilemanager<lab9.blue.blue>("txt");
                var jsonmanager = new bluejsonfilemanager<lab9.blue.blue>("json");

                var folder = path.combine(path.gettemppath(), $"bluemix_{i}");
                directory.createdirectory(folder);

                txtmanager.selectfolder(folder);

                var w = new lab10.blue.blue<lab9.blue.blue>(txtmanager, _tasks);

                w.savetasks();

                w.changemanager(jsonmanager);
                w.loadtasks();

                bool allnull = true;
                bool allsame = true;
                for (int j = 0; j < w.tasks.length; j++)
                {
                    if (w.tasks[j] != null)
                    {
                        allnull = false;
                        if (w.tasks[j].input == _tasks[j].input)
                            allsame = false;
                    }
                }
                assert.istrue(allnull, "change manager and format crash deserialization");
                if (!allnull)
                    assert.isfalse(allsame, "changemanager should affect deserialization due to format difference");

                directory.delete(folder, true);
            }
        }

        [testmethod]
        public void test_05_clear_all()
        {
            for (int i = 0; i < _input.length; i++)
            {
                init(i);

                var manager = new bluetxtfilemanager<lab9.blue.blue>("clear");
                var folder = path.combine(path.gettemppath(), $"blueclear_{i}");
                directory.createdirectory(folder);

                manager.selectfolder(folder);

                var w = new lab10.blue.blue<lab9.blue.blue>(manager, _tasks);

                w.clear();

                assert.areequal(0, w.tasks.length, $"clear failed test {i}");
                assert.isfalse(directory.exists(folder), $"folder not deleted test {i}");
            }
        }
    }
}
