集合类是专门用于数据存储和检索的类。 
#### 动态数组（ArrayList）: 
        可以使用索引在指定的位置添加和移除项目，动态数组会自动重新调整它的大小。允许在列表中进行动态内存分配、增加、搜索、排序各项。
        
#### 哈希表（Hashtable）: 
     用键来访问集合中的元素。 
     

```
 Hashtable ht = new Hashtable();
 ht.Add("001", "Zara Ali");
 ht.Add("002", "Abida Rehman");
 ht.Add("003", "Joe Holzner");
 if (ht.ContainsValue("Nuha Ali")) {
 Console.WriteLine("This student name is already in the list");
 } 
else {
 ht.Add("008", "Nuha Ali"); 
} 
// 获取键的集合
 ICollection key = ht.Keys;
```
#### 排序列表（SortedList）： 
以使用键和索引来访问列表中的项，排序列表是数组和哈希表的组合 
   ```
    SortedList sl = new SortedList();
     sl.Add("001", "Zara Ali");
     sl.Add("002", "Abida Rehman");
     sl.Add("003", "Joe Holzner");
     sl.Add("004", "Mausam Benazir Nur");
    if (sl.ContainsValue("Nuha Ali")) {
            Console.WriteLine("This student name is already in the list");
             }
           else { sl.Add("008", "Nuha Ali"); }

     // 获取键的集合
     ICollection key = sl.Keys;

     foreach (string k in key)
     {
        Console.WriteLine(k + ": " + sl[k]);
     }
```
堆栈（Stack）： 后进先出的对象集合。在列表中添加一项，称为推入元素，当从列表中移除一项时，称为弹出元素。
``` 
   Stack st = new Stack();
        st.Push('A');
        st.Push('M');
        st.Push('G');
         st.Pop();
        st.Pop();
        st.Pop();
```
队列（Queue）： 代表了一个先进先出的对象集合 
``` 
Queue q = new Queue();
  q.Enqueue('A');
  q.Enqueue('M');
foreach (char c in q) 
    Console.Write(c + " "); 
q.Enqueue('V'); 
q.Enqueue('H');
```
